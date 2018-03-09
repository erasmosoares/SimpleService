﻿using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleService.Dto;
using SimpleService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace SimpleService.Controllers
{
    /// <summary>
    /// Diff services 
    /// Custom endpoints
    /// </summary>
    public class MainObjectController : ApiController
    {
        private ApplicationDbContext _context;

        public MainObjectController()
        {
            _context = new ApplicationDbContext();
        }

        /// <summary>
        /// Retrieve all Data
        /// </summary>
        /// <returns>List of objects stored for diff usage</returns>
        [Route("v1/main/diff/data")]
        public IEnumerable<MainObjectDto> GetObjects()
        {
            return _context.MainObjects
                .ToList()
                .Select(Mapper.Map<MainObject, MainObjectDto>);
        }

        /// <summary>
        /// Retrieve a single data
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Action result</returns>
        [Route("v1/main/diff/data/{id}")]
        public IHttpActionResult GetObjects(int id)
        {
            var obj = _context.MainObjects.SingleOrDefault(c => c.Id == id);

            if (obj == null)
                return NotFound();

            return Ok(Mapper.Map<MainObject, MainObjectDto>(obj));
        }

        /// <summary>
        /// Add a stored object to the left side of diff
        /// </summary>
        /// <param name="id">Object id</param>
        /// <returns>Action result</returns>
        [HttpPost]
        [Route("v1/main/diff/{id}/left")]
        public IHttpActionResult AddLeft(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var objCheck = _context.MainObjects.SingleOrDefault(c => c.Id == id);

            if (objCheck == null)
                return NotFound();

            var obj = Mapper.Map<MainObject, MainObjectDto>(objCheck);

            // single data at time
            Diff.Instance.LeftJSON = JsonConvert.SerializeObject(obj); 

            return Ok(String.Format(" File {0} added to the left side ", obj.Id));
        }

        /// <summary>
        /// Add a stored object to the right side of Diff
        /// </summary>
        /// <param name="id">Object id</param>
        /// <returns>Action result</returns>
        [HttpPost]
        [Route("v1/main/diff/{id}/right")]
        public IHttpActionResult AddRight(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var objCheck = _context.MainObjects.SingleOrDefault(c => c.Id == id);

            if (objCheck == null)
                return NotFound();

            var obj = Mapper.Map<MainObject, MainObjectDto>(objCheck);

            // single data at time
            Diff.Instance.RightJSON = JsonConvert.SerializeObject(obj);

            return Ok(String.Format(" File {0} added to the right side ", obj.Id));
        }

        /// <summary>
        /// Perform a diff comparisson and create a result uri
        /// </summary>
        /// <param name="id">Get diffs comparing to the other side of this id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/main/diff/{id}")]
        public IHttpActionResult DiffData(int id)
        {
            Diff _data = Diff.Instance;

            String diffProperties = Diff.Instance.Compare().ToString();

            return Created(new Uri(Request.RequestUri + "/" + id), diffProperties);
        }

        /// <summary>
        /// Create a MainObject 
        /// </summary>
        /// <param name="objectDto">MainObject</param>
        /// <returns>Action result</returns>
        [HttpPost]
        [Route("v1/main/diff/data/post")]
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

        /// <summary>
        /// Update a MianObject
        /// </summary>
        /// <param name="id">Object id</param>
        /// <param name="objectDto"></param>
        [HttpPut]
        [Route("v1/main/diff/data/put")]
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

        /// <summary>
        /// Delete and object
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("v1/main/diff/data/delete")]
        public void DeleteObject(int id)
        {
            var objectInDb = _context.MainObjects.SingleOrDefault(c => c.Id == id);

            if (objectInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.MainObjects.Remove(objectInDb);
            _context.SaveChanges();
        }
    }
}
