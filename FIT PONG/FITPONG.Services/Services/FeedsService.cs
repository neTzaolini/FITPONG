﻿using AutoMapper;
using FIT_PONG.Database;
using FIT_PONG.WebAPI.Services.Bazni;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FIT_PONG.Services.Services
{
    public class FeedsService:BaseService<SharedModels.Feeds,Database.DTOs.Feed,object>, IFeedsService
    {
        public FeedsService(MyDb db, IMapper mapko):base(db,mapko)
        {

        }
        
    }
}
