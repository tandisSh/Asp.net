﻿using ElinorStoreServer.Data.Domain;
using ElinorStoreServer.Data.Entities;
using ElinorStoreServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using share.Models.Category;

namespace IbulakStoreServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _CategoryService;
        public CategoryController(CategoryService CategoryService)
        {
            _CategoryService = CategoryService;
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _CategoryService.GetAsync(id);
            return Ok(result);
        }

        [Authorize()]
        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var result = await _CategoryService.GetsAsync();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddRequestDto Category)
        {
            await _CategoryService.AddAsync(Category);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] Category Category)
        {
            await _CategoryService.EditAsync(Category);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _CategoryService.DeleteAsync(id);
            return Ok();
        }
    }
}