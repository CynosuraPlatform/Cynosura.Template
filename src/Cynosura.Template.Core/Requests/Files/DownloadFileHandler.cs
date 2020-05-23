using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Data;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.FileStorage;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.Files.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cynosura.Template.Core.Requests.Files
{
    public class DownloadFileHandler : IRequestHandler<DownloadFile, FileContentModel>
    {
        private readonly IEntityRepository<File> _fileRepository;
        private readonly IFileStorage _fileStorage;

        public DownloadFileHandler(IEntityRepository<File> fileRepository,
            IFileStorage fileStorage)
        {
            _fileRepository = fileRepository;
            _fileStorage = fileStorage;
        }

        public async Task<FileContentModel> Handle(DownloadFile request, CancellationToken cancellationToken)
        {
            var file = await _fileRepository.GetEntities()
                .Include(e => e.Group)
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync();
            byte[] content = null;
            if (file.Group.Type == Enums.FileGroupType.Database)
            {
                content = file.Content;
            }
            else if (file.Group.Type == Enums.FileGroupType.Storage)
            {
                content = await _fileStorage.DownloadFileAsync(file.Url);
            }
            else
            {
                throw new NotSupportedException($"Group type {file.Group.Type} not supported");
            }
            return new FileContentModel
            {
                Name = file.Name,
                ContentType = file.ContentType,
                Content = content,
            };
        }

    }
}
