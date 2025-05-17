using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System.Web.Http.Results;
using TransVault.Application.DTOs;
using TransVault.Application.Interfaces;
using TransVault.Common;
using TransVault.Domain.Entities;
using TransVault.Infrastructure.Common.Filtring;

namespace TransVault.Controllers
{
    [Route(ApiRoutes.ControllerRoute)]
    [ApiController]
    public class IndexedFieldController : BaseApiController
    {
        private readonly IIndexedFieldService _indexedFieldService;

        public IndexedFieldController(IIndexedFieldService indexedFieldService)
        {
            _indexedFieldService = indexedFieldService;
        }

        [HttpGet(ApiRoutes.SearchIndexedField)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QueryListResponse<IndexedFieldDto>))]
        public async Task<IActionResult> GetIndexedFields([FromQuery] APIQueryParamters apiQueryParamters)
        {
            var indexedFields = await _indexedFieldService.GetAllAsync(apiQueryParamters);
            return OKResponse(indexedFields);
        }

        [HttpGet(ApiRoutes.IndexedField)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IndexedFieldDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetIndexedField(int documentId)
        {
            var indexedField = await _indexedFieldService.GetByDocumentIdAsync(documentId);
            if (indexedField == null)
            {
                return ErrorResponse(System.Net.HttpStatusCode.NotFound,string.Format("Index with document id {0} not found",documentId));
            }
            return OKResponse(indexedField);

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IndexedFieldDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddIndexedField([FromBody] IndexedFieldDto indexedFieldDto)
        {
            if (!ModelState.IsValid)
            {
                return ErrorResponse(ModelState,System.Net.HttpStatusCode.BadRequest,"Model state not valid");
            }
            var addedIndexedField=await _indexedFieldService.AddAsync(indexedFieldDto);
            return OKResponse(addedIndexedField);
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdatedIndexFieldRequest))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateIndexedField([FromBody] UpdatedIndexFieldRequest updatedIndexFieldRequest)
        {
            if (!ModelState.IsValid)
            {
                return ErrorResponse(ModelState, System.Net.HttpStatusCode.BadRequest, "Model state not valid");
            }
            var updatedModel = await _indexedFieldService.Update(updatedIndexFieldRequest);
            if (updatedModel == null)
            {
                return ErrorResponse(ModelState, System.Net.HttpStatusCode.NotFound, "Update failed");
            }
            return OKResponse(updatedModel);
        }
    }
}