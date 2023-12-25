using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pronia.Application.Abstractions.Repositories;
using Pronia.Application.Abstractions.Services;
using Pronia.Application.DTOs;
using Pronia.Domain.Entities;
using Pronia.Persistence.Exceptions;

namespace Pronia.Persistence.Implementations.Services;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;
    private readonly IMapper _mapper;

    public TagService(ITagRepository tagRepository, IMapper mapper)
    {
        _tagRepository = tagRepository;
        _mapper = mapper;
    }

    public async Task CreateAsync(TagPostDto dto)
    {
        bool isExist = await _tagRepository.IsExistAsync(x => x.Name == dto.Name);
        if (isExist)
            throw new TagAlreadyExistException();
        var tag = _mapper.Map<Tag>(dto);
        await _tagRepository.CreateAsync(tag);
        await _tagRepository.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var tag = await _tagRepository.GetSingleAsync(x => x.Id == id);
        if (tag is null)
            throw new TagNotFoundException();
        _tagRepository.HardDelete(tag);
        await _tagRepository.SaveAsync();
    }

    public async Task<List<TagGetDto>> GetAllAsync()
    {
        var tags = await _tagRepository.OrderBy(_tagRepository.GetAll(), x => x.Id).ToListAsync();
        var dtos = _mapper.Map<List<TagGetDto>>(tags);
        return dtos;
    }

    public async Task<List<TagGetDto>> GetAllAsync(int limit, int page)
    {
        var tags = await _tagRepository.Paginate(_tagRepository.GetAll(), limit, page).ToListAsync();
        var dtos = _mapper.Map<List<TagGetDto>>(tags);
        return dtos;
    }

    public async Task<TagGetDto> GetAsync(int id)
    {
        var tag = await _tagRepository.GetSingleAsync(x => x.Id == id);
        if (tag is null)
            throw new TagNotFoundException();
        var dto = _mapper.Map<TagGetDto>(tag);

        return dto;
    }

    public async Task<bool> IsExistAsync(int id)
    {
        return await _tagRepository.IsExistAsync(x => x.Id == id);  
    }

    public async Task UpdateAsync(TagPutDto dto)
    {
        var existedTag = await _tagRepository.GetSingleAsync(x => x.Id == dto.Id);
        if (existedTag is null)
            throw new TagNotFoundException();

        bool isExist = await _tagRepository.IsExistAsync(x => x.Name == dto.Name && x.Id != dto.Id);
        if (isExist)
            throw new TagAlreadyExistException();

        existedTag = _mapper.Map<TagPutDto, Tag>(dto, existedTag);
        _tagRepository.Update(existedTag);
        await _tagRepository.SaveAsync();
    }
}
