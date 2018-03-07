using AutoMapper;
using Newtonsoft.Json.Linq;
using SimpleService.Dto;
using SimpleService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleService.Controllers
{
    public class MainObjectController : ApiController
    {
        private ApplicationDbContext _context;

        public MainObjectController()
        {
            _context = new ApplicationDbContext();
        }

        [Route("v1/diff/data")]
        public IEnumerable<MainObjectDto> GetObjects()
        {
            return _context.MainObjects
                .ToList()
                .Select(Mapper.Map<MainObject, MainObjectDto>);
        }

        [Route("v1/diff/data/{id}")]
        public IHttpActionResult GetObjects(int id)
        {
            var obj = _context.MainObjects.SingleOrDefault(c => c.Id == id);

            if (obj == null)
                return NotFound();

            return Ok(Mapper.Map<MainObject, MainObjectDto>(obj));
        }

        [HttpPost]
        [Route("v1/diff/{id}/left")]
        public IHttpActionResult AddLeft(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var objCheck = _context.MainObjects.SingleOrDefault(c => c.Id == id);

            if (objCheck == null)
                return NotFound();

            var obj = Mapper.Map<MainObject, MainObjectDto>(objCheck);

            Diff.Instance.LeftJSON = obj.ToString();

            return Ok(String.Format(" File {0} added to the left side ", obj.Id));
        }

        [HttpPost]
        [Route("v1/diff/{id}/right")]
        public IHttpActionResult AddRight(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var objCheck = _context.MainObjects.SingleOrDefault(c => c.Id == id);

            if (objCheck == null)
                return NotFound();

            var obj = Mapper.Map<MainObject, MainObjectDto>(objCheck);

            Diff.Instance.RightJSON = obj.ToString();

            return Ok(String.Format(" File {0} added to the right side ", obj.Id));
        }

        [HttpPost]
        [Route("v1/diff/{id}")]
        public IHttpActionResult DiffData(int id)
        {
           IEnumerable<JProperty> diffProperties = Diff.Instance.Compare();

            foreach (var item in diffProperties)
            {
                Console.WriteLine(" -> "+item.Value);
            }

           return Created(new Uri(Request.RequestUri + "/" + id), new Result { Diff= false } );
        }

        [HttpPost]
        [Route("v1/diff/data/post")]
        public IHttpActionResult CreateObject(MainObjectDto objectDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var obj = Mapper.Map<MainObjectDto, MainObject>(objectDto);
            _context.MainObjects.Add(obj);
            _context.SaveChanges();

            objectDto.Id = obj.Id;

            return Created(new Uri(Request.RequestUri + "/" + obj.Id), objectDto);

        }

        [HttpPut]
        [Route("v1/diff/data/put")]
        public void UpdateObject(int id, MainObjectDto objectDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var objectInDb = _context.MainObjects.SingleOrDefault(c => c.Id == id);

            if (objectInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(objectDto, objectInDb);

            _context.SaveChanges();
        }

        [HttpDelete]
        [Route("v1/diff/data/delete")]
        public void DeleteObject(int id)
        {
            var objectInDb = _context.MainObjects.SingleOrDefault(c => c.Id == id);

            if (objectInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.MainObjects.Remove(objectInDb);
            _context.SaveChanges();
        }
    }

    public class Result
    {
        public bool Diff { get; set; }
    }
}
