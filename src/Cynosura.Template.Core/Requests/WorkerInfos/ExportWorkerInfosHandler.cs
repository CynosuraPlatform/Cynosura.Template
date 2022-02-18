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
using Cynosura.Template.Core.Requests.WorkerInfos.Models;

namespace Cynosura.Template.Core.Requests.WorkerInfos
{
    public class ExportWorkerInfosHandler : IRequestHandler<ExportWorkerInfos, FileContentModel>
    {
        private readonly IEntityRepository<WorkerInfo> _workerInfoRepository;
        private readonly IExcelFormatter _excelFormatter;
        private readonly IMapper _mapper;

        public ExportWorkerInfosHandler(IEntityRepository<WorkerInfo> workerInfoRepository,
            IExcelFormatter excelFormatter,
            IMapper mapper)
        {
            _workerInfoRepository = workerInfoRepository;
            _excelFormatter = excelFormatter;
            _mapper = mapper;
        }

        public async Task<FileContentModel> Handle(ExportWorkerInfos request, CancellationToken cancellationToken)
        {
            IQueryable<WorkerInfo> query = _workerInfoRepository.GetEntities();            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var workerInfos = await query.ToListAsync();
            var models = _mapper.Map<List<WorkerInfo>, List<WorkerInfoModel>>(workerInfos);
            return await _excelFormatter.GetExcelFileAsync(models, "WorkerInfos");
        }

    }
}
