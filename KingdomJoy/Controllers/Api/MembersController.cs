using AutoMapper;
using JQDT.WebAPI;
using KingdomJoy.Dtos;
using KingdomJoy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace KingdomJoy.Controllers.Api
{
    public class MembersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
  
        //public MembersController(MapperConfiguration config)
        //{
        //    this.config = config;
        //}
        //private MapperConfiguration config;

        public MembersController()
        {

        }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<object> Post(MemberDto data)
        {

            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Member, MemberDto>();
                    cfg.CreateMap<MemberDto, Member>();
                });

                IMapper mapper = new Mapper(config);
                var member = mapper.Map<Member>(data);


                //var member = new Member
                //{
                //    FirstName = data.FirstName,
                //    LastName = data.LastName,
                //    MiddleName = data.MiddleName,
                //    Gender = data.Gender,
                //    Dob = data.Dob,
                //    Dom = data.Dom,
                //    Phone = data.Phone,
                //    Phone2 = data.Phone2,
                //    Email = data.Email,
                //    BornAgain = data.BornAgain,
                //    Baptized = data.Baptized,
                //    Status = data.Status,
                //    TitleID = data.TitleID,
                //    Designation = data.Designation,
                //    designation = data.Designation,
                //    FoundationSchool = data.FoundationSchool,
                //    Address = data.Address

                //};


                db.Members.Add(member);
                await db.SaveChangesAsync();
               
                ResponseObject response = new ResponseObject { Message = "Success", Data = data };
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            else
            {
                ResponseObject response = new ResponseObject { Message = "Error", Data = data };
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            var member = await db.Members.FindAsync(id);
            if(member != null)
            {
                db.Members.Remove(member);
                await db.SaveChangesAsync();
                return Ok("Deleted");
            }
            else
            {
                return BadRequest("Member not found");
            }
  
        }

        [JQDataTable]
        [Route("api/members/dataTablesServerSide")]
        [HttpPost]
        public IHttpActionResult DataTables([FromBody]string data)
        {
            IQueryable<Member> result =  db.Members;
            return this.Ok(result);
        }

        [Route("api/members/dataTablesBasic")]
        [HttpPost]
        public async Task<IHttpActionResult> DataTablesBasic([FromBody]string data)
        {
            IEnumerable<Member> result = await db.Members.ToListAsync();

            IList<MemberDataTablesDto> memberDataTable = new List<MemberDataTablesDto>();

            foreach(var item in result)
            {
                memberDataTable.Add(new MemberDataTablesDto
                {
                    Id = item.Id.ToString(),
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Gender = item.Gender == 1 ? "Male" : "Female",
                    Status = item.Status == 1 ? "Yes" : "No",
                    FoundationSchool = item.FoundationSchool == 1 ? "Yes" : "No",
                    BornAgain = item.BornAgain == 1 ? "Yes" : "No",
                    Baptized = item.Baptized == 1 ? "Yes" : "No",
                    Address = item.Address,
                    Title = item.Title.Name,
                    Dob = item.Dob.ToShortDateString(),
                    Dom =item.Dom.ToShortDateString(),
                    Email =item.Email,
                    Phone =item.Phone,
                    Phone2 =item.Phone2,
                    MiddleName =item.MiddleName,
                    Designation =item.designation.Name,
                    Edit = $"<div class=dropdown>" +
                            $"<a href=\"\" class=\"tx-gray-800 d-inline-block\" data-toggle=\"dropdown\">" +
                            $"<div class=\"ht-45 pd-x-10 bd d-flex align-items-center justify-content-center\">" +
                             $"<span class=\"mg-r-10 tx-13 tx-medium\">Edit" +
                             $"<i class=\"fa fa-angle-down mg-l-10\"></i></span>" +
                             $"</div></a>" +
                             $"<div class=\"dropdown-menu pd-10 wd-100\">" +
                            $"<nav class=\"nav nav-style-1 flex-column\">" +
                            $"<a class=\"btn btn-link nav-link edit\" data-id={item.Id}>Edit</a><a class=\"btn btn-link nav-link delete\" data-id={item.Id}>Delete</a></nav></div></div>"


                });
            }

            return Ok(memberDataTable);
        }



    }

    public class ResponseObject
    {
        public string Message { get; set; }
        public object Data { get; set; }
    }


}