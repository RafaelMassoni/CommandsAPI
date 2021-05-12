using System.Collections.Generic;
using AutoMapper;
using Commands.Data;
using Commands.DTOs;
using Commands.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commands.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandsRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommandsRepo repository, IMapper mapper)
        {
            _repository = repository;  
            _mapper = mapper; 
        }

        [HttpGet]
        public ActionResult <IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandList = _repository.GetAllCommands();
            if(commandList != null)
                return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandList));
            
            return NotFound();
        }

        [HttpGet("{id}", Name="GetCommandById")]
        public ActionResult <CommandReadDto> GetCommandById(int id)
        {
            var command = _repository.GetCommandById(id);

            if(command != null)
                return Ok(_mapper.Map<CommandReadDto>(command));
            
            return NotFound();
        }

        [HttpPost]
        public ActionResult <CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            return CreatedAtRoute(nameof(GetCommandById), new {Id = commandModel.Id}, commandReadDto);
        } 

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            
            if(commandModelFromRepo == null)
                return NotFound();
            
            _mapper.Map(commandUpdateDto, commandModelFromRepo);

            _repository.UpdateCommand(commandModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialUpdateCommand(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            
            if(commandModelFromRepo == null)
                return NotFound();
            
            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
            patchDoc.ApplyTo(commandToPatch);

            if(!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, commandModelFromRepo);

            _repository.UpdateCommand(commandModelFromRepo);
            
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<CommandReadDto> DeleteCommand(int id)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);

            if(commandModelFromRepo == null)
                return NotFound();

            _repository.DeleteCommand(commandModelFromRepo);
            _repository.SaveChanges();

            var deletedCommand = _mapper.Map<CommandReadDto>(commandModelFromRepo);
            return Ok(deletedCommand);
        }
    }
}