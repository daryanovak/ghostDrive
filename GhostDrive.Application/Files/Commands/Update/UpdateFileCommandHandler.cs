using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GhostDrive.Application.Constants;
using GhostDrive.Application.Models;
using GhostDrive.Domain.Models;
using GhostDrive.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GhostDrive.Application.Files.Commands.Update
{
    public class UpdateFileCommandHandler : IRequestHandler<UpdateFileCommand, CommandResult>
    {
        private const char TagSeparator = ' ';

        private readonly GhostDriveDbContext _context;

        public UpdateFileCommandHandler(GhostDriveDbContext context)
        {
            _context = context;
        }

        public async Task<CommandResult> Handle(UpdateFileCommand request, CancellationToken cancellationToken)
        {
            var file = await _context.Files.Include(f => f.Tags).ThenInclude(f => f.Tag)
                .Where(f => f.Id == request.FileId).FirstOrDefaultAsync(cancellationToken);

            if (file == null)
            {
                return CommandResult.Fail(CommandErrors.FileNotFound);
            }

            var tagNames = request.Tags.Split(TagSeparator).Where(tag => !string.IsNullOrWhiteSpace(tag));
            file.Tags.Clear();
            foreach (var tagName in tagNames)
            {
                var tag = GetOrAddTag(tagName);
                file.Tags.Add(new FileTag {File = file, Tag = tag});
            }

            await _context.SaveChangesAsync(cancellationToken);

            return CommandResult.Success;
        }

        private Tag GetOrAddTag(string tagName)
        {
            var tag = _context.Tags.FirstOrDefault(x => x.Name == tagName);
            if (tag == null)
            {
                tag = new Tag {Name = tagName};
                _context.Tags.Add(tag);
            }
            return tag;
        }
    }
}
