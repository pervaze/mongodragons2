using System.Collections.Generic;
using System;
using System.Net;
using System.Web.Http;
using MongoDragons2.Repository;
using MongoDragons2.Types;
using MongoDragons2.Models;

namespace MongoDragons2.Controllers
{
    public class DragonController : ApiController
    {
        public ResponseModel<IEnumerable<Dragon>> Get()
        {
            IEnumerable<Dragon> dragons = DragonRepository.ToList();
            return new ResponseModel<IEnumerable<Dragon>>()
            {
                data = dragons,
                status = true
            };
        }

        public Dragon Spawn()
        {
            Dragon dragon = DragonRepository.Spawn();
            return dragon;
        }

        public ResponseModel<object> Remove(Dragon dragon)
        {
            ResponseModel<object> response = new ResponseModel<object>();
            response.status = DragonRepository.Remove(dragon);
            if (response.status != true)
            {
                response.statusMessage = "Error Occured";
            }
            return response;
        }
    }
}
