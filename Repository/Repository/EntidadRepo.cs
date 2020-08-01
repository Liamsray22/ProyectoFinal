using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Repository.Repository
{
    //public class EntidadRepo : RepositoryBase<Entidad, Context>
    //{
    //    private readonly Context _context;
    //    private readonly UserManager<IdentityUser> _userManager;
    //    private readonly SignInManager<IdentityUser> _signInManager;
    //    private readonly UsuarioRepo _usuarioRepo;
    //    private readonly IMapper _mapper;


    //    public EntidadRepo(Context context, UserManager<IdentityUser> userManager,
    //                        SignInManager<IdentityUser> signInManager, IMapper mapper,
    //                        UsuarioRepo usuarioRepo) : base(context)
    //    {
    //        _context = context;
    //        _userManager = userManager;
    //        _signInManager = signInManager;
    //        _mapper = mapper;
    //        _usuarioRepo = usuarioRepo;
    //    }

    //    public void Borrar(string path)
    //    {
    //        File.SetAttributes(path, FileAttributes.Normal);
    //        System.GC.Collect();
    //        System.GC.WaitForPendingFinalizers();

    //        File.Delete(path);
    //    }


    //}
}
