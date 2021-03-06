﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BackendOfTH6.Models;

namespace BackendOfTH6.Controllers
{
    public class DanhMucsController : ApiController
    {
        private Th6Entities db = new Th6Entities();

        // GET: api/DanhMucs
        public IQueryable<DanhMuc> GetDanhMuc()
        {
            return db.DanhMuc;
        }

        // GET: api/DanhMucs/5
        [ResponseType(typeof(DanhMuc))]
        public IHttpActionResult GetDanhMuc(int id)
        {
            DanhMuc danhMuc = db.DanhMuc.Find(id);
            if (danhMuc == null)
            {
                return NotFound();
            }

            return Ok(danhMuc);
        }

        // PUT: api/DanhMucs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDanhMuc(int id, DanhMuc danhMuc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != danhMuc.MaDanhMuc)
            {
                return BadRequest();
            }

            db.Entry(danhMuc).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DanhMucExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/DanhMucs
        [ResponseType(typeof(DanhMuc))]
        public IHttpActionResult PostDanhMuc(DanhMuc danhMuc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DanhMuc.Add(danhMuc);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DanhMucExists(danhMuc.MaDanhMuc))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = danhMuc.MaDanhMuc }, danhMuc);
        }

        // DELETE: api/DanhMucs/5
        [ResponseType(typeof(DanhMuc))]
        public IHttpActionResult DeleteDanhMuc(int id)
        {
            DanhMuc danhMuc = db.DanhMuc.Find(id);
            if (danhMuc == null)
            {
                return NotFound();
            }

            db.DanhMuc.Remove(danhMuc);
            db.SaveChanges();

            return Ok(danhMuc);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DanhMucExists(int id)
        {
            return db.DanhMuc.Count(e => e.MaDanhMuc == id) > 0;
        }
    }
}