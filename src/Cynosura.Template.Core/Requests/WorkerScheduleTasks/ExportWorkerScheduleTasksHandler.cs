﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using Cynosura.Core.Services.Models;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Formatters;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.WorkerScheduleTasks.Models;

namespace Cynosura.Template.Core.Requests.WorkerScheduleTasks
{
    public class ExportWorkerScheduleTasksHandler : IRequestHandler<ExportWorkerScheduleTasks, FileContentModel>
    {
        private readonly IEntityRepository<WorkerScheduleTask> _workerScheduleTaskRepository;
        private readonly IExcelFormatter _excelFormatter;
        private readonly IMapper _mapper;

        public ExportWorkerScheduleTasksHandler(IEntityRepository<WorkerScheduleTask> workerScheduleTaskRepository,
            IExcelFormatter excelFormatter,
            IMapper mapper)
        {
            _workerScheduleTaskRepository = workerScheduleTaskRepository;
            _excelFormatter = excelFormatter;
            _mapper = mapper;
        }

        public async Task<FileContentModel> Handle(ExportWorkerScheduleTasks request, CancellationToken cancellationToken)
        {
            IQueryable<WorkerScheduleTask> query = _workerScheduleTaskRepository.GetEntities()
                .Include(e => e.WorkerInfo);            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var workerScheduleTasks = await query.ToListAsync(cancellationToken);
            var models = _mapper.Map<List<WorkerScheduleTask>, List<WorkerScheduleTaskModel>>(workerScheduleTasks);
            return await _excelFormatter.GetExcelFileAsync(models, "WorkerScheduleTasks");
        }

    }
}
