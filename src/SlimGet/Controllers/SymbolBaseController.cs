using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SlimGet.Data;
using SlimGet.Data.Configuration;
using SlimGet.Filters;
using SlimGet.Services;

namespace SlimGet.Controllers
{
    [Route("/api/v3/symbolstore"), ApiController, AllowAnonymous, ServiceFilter(typeof(RequireSymbolsEnabled))]
    public sealed class SymbolBaseController : NuGetControllerBase
    {
        public SymbolBaseController(SlimGetContext db, RedisService redis, IFileSystemService fs, IOptions<StorageConfiguration> storcfg, ILoggerFactory logger)
            : base(db, redis, fs, storcfg, logger)
        { }

        [Route(""), HttpGet]
        public IActionResult Dummy() => this.NoContent();

        [Route("index2.txt"), HttpGet]
        public IActionResult Index2()
            => this.Content("", "text/plain", Utilities.UTF8);

        [Route("pingme.txt"), HttpGet]
        public IActionResult PingMe()
            => this.Content("", "text/plain", Utilities.UTF8);

        [Route("{file}/{sig}/{file2}"), Route("{t2prefix}/{file}/{sig}/{file2}"), HttpGet]
        public async Task<IActionResult> Symbols(string file, string sig, string file2, CancellationToken cancellationToken)
        {
            if (sig.Length != 33 && sig.Length != 40)
                return this.BadRequest();

            if (!Guid.TryParseExact(sig.AsSpan(0, 32), "N", out var id))
                return this.BadRequest();

            var symbols = await this.Database.PackageSymbols
                .Include(x => x.Binary)
                .ThenInclude(x => x.Package)
                .FirstOrDefaultAsync(x => x.Identifier == id, cancellationToken).ConfigureAwait(false);
            if (symbols == null)
                return this.NotFound();

            if (symbols.Name != file)
                return this.NotFound();

            var pdb = this.FileSystem.OpenSymbolsRead(new PackageInfo(symbols.PackageId, symbols.Binary.Package.NuGetVersion), id);
            return this.File(pdb, "application/octet-stream", symbols.Name);
        }
    }
}
