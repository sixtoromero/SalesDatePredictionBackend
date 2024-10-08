﻿using AIRIS.DocBD.Application.DTO;
using AIRIS.DocBD.Application.Interface;
using AIRIS.DocBD.Services.WebAPIRest.Helpers;
using AIRIS.DocBD.Transversal.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AIRIS.DocBD.Services.WebAPIRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClientesApplication _Application;
        private readonly AppSettings _appSettings;
        private readonly IWebHostEnvironment env;

        public ClientesController(IClientesApplication _Application,
                                  IOptions<AppSettings> appSettings,
                                  IWebHostEnvironment env)
        {
            this._Application = _Application;
            _appSettings = appSettings.Value;
            this.env = env;
        }

        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            Response<IEnumerable<ClienteDTO>> response = new Response<IEnumerable<ClienteDTO>>();

            try
            {
                response = await _Application.GetAllAsync();
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.IsSuccess = false;
                response.Message = ex.Message;

                return BadRequest(response);
            }
        }

        [HttpGet("GetAsync")]
        public async Task<IActionResult> GetAsync(int ID)
        {
            Response<ClienteDTO> response = new Response<ClienteDTO>();

            try
            {
                response = await _Application.GetAsync(ID);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.IsSuccess = false;
                response.Message = ex.Message;

                return BadRequest(response);
            }
        }

        [HttpPost("InsertAsync")]        
        public async Task<IActionResult> InsertAsync(IFormFile File, [FromForm] string jsonModel)
        {
            ClienteDTO modelDto = Newtonsoft.Json.JsonConvert.DeserializeObject<ClienteDTO>(jsonModel);

            Response<bool> response = new Response<bool>();
            string fileExt = string.Empty;
            string fileName = Guid.NewGuid().ToString();
            string hosturl = "https://localhost:7294/Resources/images/";

            try
            {
                if (File != null)
                {
                    if (File.Length > 0)
                    {
                        fileExt = Path.GetExtension(File.FileName);
                        string pathDirectory = Path.Combine(env.WebRootPath, $"Resources\\images\\");                        

                        if (!Directory.Exists(pathDirectory))
                        {
                            Directory.CreateDirectory(pathDirectory);
                        }
                          
                        //Validando si existe el archivo.
                        var fileExists = Path.Combine(pathDirectory, fileName);
                        FileInfo fi = new FileInfo(fileExists);
                        if (fi.Exists)
                        {
                            System.IO.File.Delete(fileExists);
                            fi.Delete();
                        }

                        var uploading = Path.Combine(pathDirectory, fileName + fileExt);
                        
                        modelDto!.Foto = Path.Combine(hosturl, fileName + fileExt);

                        using (var stream = new FileStream(uploading, FileMode.Create))
                        {
                            await File.CopyToAsync(stream);
                        }
                    }
                }

                if (modelDto == null)
                    return BadRequest();

                response = await _Application.InsertAsync(modelDto);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.IsSuccess = false;
                response.Message = ex.Message;

                return BadRequest(response);
            }
        }

        [HttpPut("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(IFormFile File, [FromForm] string jsonModel)
        {
            ClienteDTO modelDto = Newtonsoft.Json.JsonConvert.DeserializeObject<ClienteDTO>(jsonModel);
            Response<bool> response = new Response<bool>();
            string fileExt = string.Empty;
            string hosturl = "https://localhost:7294/Resources/images/";
            string fileName = Guid.NewGuid().ToString();
            
            var fileResp = modelDto.Foto.Split('/');

            try
            {
                if (File != null)
                {
                    if (File.Length > 0)
                    {
                        fileExt = Path.GetExtension(File.FileName);
                        string pathDirectory = Path.Combine(env.WebRootPath, $"Resources\\images\\");
                        if (!Directory.Exists(pathDirectory))
                        {
                            Directory.CreateDirectory(pathDirectory);
                        }

                        var fileExists = Path.Combine(pathDirectory, fileName);
                        FileInfo fi = new FileInfo(fileExists);
                        if (fi.Exists)
                        {
                            System.IO.File.Delete(fileExists);
                            fi.Delete();
                        }

                        var uploading = Path.Combine(pathDirectory, fileName + fileExt);
                        modelDto!.Foto = Path.Combine(hosturl, fileName + fileExt);

                    }
                }
                if (modelDto == null)
                    return BadRequest();

                response = await _Application.UpdateAsync(modelDto);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.IsSuccess = false;
                response.Message = ex.Message;

                return BadRequest(response);
            }
        }

        [HttpDelete("DelAsync")]
        public async Task<IActionResult> DelAsync(int Id, string fileName)
        {
            Response<bool> response = new Response<bool>();            
            try
            {
                string pathDirectory = Path.Combine(env.WebRootPath, $"Resources\\images\\");
                if (Directory.Exists(pathDirectory))
                {
                    var fileExists = Path.Combine(pathDirectory, fileName);
                    FileInfo fi = new FileInfo(fileExists);
                    if (fi.Exists)
                    {
                        System.IO.File.Delete(fileExists);
                        fi.Delete();                        
                    }                    
                }

                response = await _Application.DeleteAsync(Id);
                if (response.IsSuccess)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.IsSuccess = false;
                response.Message = ex.Message;

                return BadRequest(response);
            }
        }
    }    
}
