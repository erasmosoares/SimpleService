using AutoMapper;
using Newtonsoft.Json;
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
    public class JSONFileController : ApiController
    {
        private ApplicationDbContext _context;

        public JSONFileController()
        {
            _context = new ApplicationDbContext();
        }

        /// <summary>
        /// Retrieve all Data
        /// </summary>
        /// <returns>List of JSON's stored for diff usage</returns>
        [Route("v1/diff/data")]
        public IEnumerable<JSONFileDto> GetFiles()
        {
            return _context.Files.ToList().Select(Mapper.Map<JSONFile, JSONFileDto>);
        }

        /// <summary>
        /// Retrieve a single JSON data
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Action result</returns>
        [Route("v1/diff/data/{id}")]
        public IHttpActionResult GetFiles(int id)
        {
            var obj = _context.Files.SingleOrDefault(c => c.Id == id);

            if (obj == null)
                return NotFound();

            return Ok(Mapper.Map<JSONFile, JSONFileDto>(obj));
        }

        /// <summary>
        /// Add a stored JSON to the left side of diff
        /// </summary>
        /// <param name="id">Object id</param>
        /// <returns>Action result</returns>
        [HttpPost]
        [Route("v1/diff/{id}/left")]
        public IHttpActionResult AddLeft(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var objCheck = _context.Files.SingleOrDefault(c => c.Id == id);

            if (objCheck == null)
                return NotFound();

            var obj = Mapper.Map<JSONFile, JSONFileDto>(objCheck);

            // single data at time
            Diff.Instance.LeftJSON = obj.File;

            return Ok(String.Format(" File {0} added to the left side ", obj.Id));
        }

        /// <summary>
        /// Add a stored file to the right side of Diff
        /// </summary>
        /// <param name="id">Object id</param>
        /// <returns>Action result</returns>
        [HttpPost]
        [Route("v1/diff/{id}/right")]
        public IHttpActionResult AddRight(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var objCheck = _context.Files.SingleOrDefault(c => c.Id == id);

            if (objCheck == null)
                return NotFound();

            var obj = Mapper.Map<JSONFile, JSONFileDto>(objCheck);

            // single data at time
            Diff.Instance.RightJSON = obj.File;

            return Ok(String.Format(" File {0} added to the right side ", obj.Id));
        }

        /// <summary>
        /// Perform a diff comparisson and create a result uri
        /// </summary>
        /// <param name="id">Get diffs comparing to the other side of this id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("v1/diff/{id}")]
        public IHttpActionResult DiffData(int id)
        {
            Diff _data = Diff.Instance;

            if(_data.LeftJSON is null || _data.RightJSON is null)
                return BadRequest();

            IEnumerable<JProperty> diffProperties = Diff.Instance.Compare();

            foreach (var item in diffProperties)
            {
                Console.WriteLine(" -> " + item.Value);
            }

            //Register register = new Register
            //{                
            //    Left = _data.LeftJSON,
            //    Right = _data.RightJSON,
            //    Result = diffProperties.ToString()
            //};

            ////Storing for log
            //_context.Registers.Add(register);

            //_context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + id), diffProperties);
        }

        #region CRUD

        /// <summary>
        /// Inser a JSON File 
        /// </summary>
        /// <param name="JSONFile">JSONFile</param>
        /// <returns>Action result</returns>
        [HttpPost]
        [Route("v1/diff/data/post")]
        public IHttpActionResult AddJSONFile(JSONFileDto JSONFile)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var obj = Mapper.Map<JSONFileDto, JSONFile>(JSONFile);
            _context.Files.Add(obj);
            _context.SaveChanges();

            JSONFile.Id = obj.Id;

            return Created(new Uri(Request.RequestUri + "/" + obj.Id), JSONFile);

        }

        /// <summary>
        /// Update a JSON Data
        /// </summary>
        /// <param name="id">JSON id</param>
        /// <param name="jsonData"></param>
        [HttpPut]
        [Route("v1/diff/data/put")]
        public void UpdateObject(int id, JSONFileDto jsonData)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var jsonInDb = _context.Files.SingleOrDefault(c => c.Id == id);

            if (jsonInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(jsonData, jsonInDb);

            _context.SaveChanges();
        }

        /// <summary>
        /// Delete 
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("v1/diff/data/delete")]
        public void DeleteObject(int id)
        {
            var jsonInDb = _context.Files.SingleOrDefault(c => c.Id == id);

            if (jsonInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Files.Remove(jsonInDb);
            _context.SaveChanges();
        }

        #endregion
    }
}
