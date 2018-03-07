using AutoMapper;
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

        public IEnumerable<MainObjectDto> GetObjects()
        {
            return _context.MainObjects
                .ToList()
                .Select(Mapper.Map<MainObject, MainObjectDto>);
        }

        public IHttpActionResult GetObjects(int id)
        {
            var customer = _context.MainObjects.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<MainObject, MainObjectDto>(customer));
        }

        [HttpPost]
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
        public void DeleteMovie(int id)
        {
            var movieInDb = _context.MainObjects.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.MainObjects.Remove(movieInDb);
            _context.SaveChanges();
        }
    }
}
