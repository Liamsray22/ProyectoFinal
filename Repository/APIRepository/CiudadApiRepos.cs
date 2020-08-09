using AutoMapper;
using DataBase.Models;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.APIRepository
{

    public class CiudadApiRepos : RepositoryBase<Ciudadanos, ItlaElectorDBContext>
    {

        private readonly ItlaElectorDBContext _context;
        private readonly IMapper _mapper;

        public CiudadApiRepos(ItlaElectorDBContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }


   


    }



}
