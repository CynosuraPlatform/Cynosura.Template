using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Data;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.Files.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cynosura.Template.Core.Requests.Files
{
    public class DownloadFileHandler : IRequestHandler<DownloadFile, FileContentModel>
    {
        private readonly IEntityRepository<File> _fileRepository;

        public DownloadFileHandler(IEntityRepository<File> fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<FileContentModel> Handle(DownloadFile request, CancellationToken cancellationToken)
        {
            var file = await _fileRepository.GetEntities()
                .Include(e => e.Group)
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync();
            if (file.Group.Type != Enums.FileGroupType.Database)
                throw new Exception("Unsupported file group");
            return new FileContentModel
            {
                Name = file.Name,
                ContentType = file.ContentType,
                Content = file.Content,
            };
        }

    }
}
