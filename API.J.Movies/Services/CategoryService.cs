using API.J.Movies.DAL.Models;
using API.J.Movies.DAL.Models.Dtos;
using API.J.Movies.Repository;
using API.J.Movies.Repository.IRepository;
using API.J.Movies.Services.IServices;
using AutoMapper;
using System.Collections;

namespace API.J.Movies.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public Task<bool> CategoryExistsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CategoryExistsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryDto> CreateCategoryAsync(CategoryCreateDto categoryCreateDto)
        {
            //Validar si la categoría ya existe
            var categoryExists = await _categoryRepository.CategoryExistsByNameAsync(categoryCreateDto.Name);

            if (categoryExists)
            {
                throw new InvalidOperationException($"Ya existe una categoría con el nombre de '{categoryCreateDto.Name}'");
            }

            //Mapear el DTO a la entidad
            var category = _mapper.Map<Category>(categoryCreateDto);

            //Crear la categoría en el repositorio
            var categoryCreated = await _categoryRepository.CreateCategoryAsync(category);

            if (!categoryCreated)
            {
                throw new Exception("Ocurrió un error al crear la categoría.");
            }

            //Mapear la entidad creada a DTO
            return _mapper.Map<CategoryDto>(category);
        }

        public Task<bool> CreateCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            await GetCategoryByIdAsync(id);

            var isDeleted = await _categoryRepository.DeleteCategoryAsync(id);

            if (!isDeleted)
            {
                throw new Exception("Ocurrió un error al eliminar la categoría.");
            }

            return isDeleted;
        }

        public async Task<ICollection<CategoryDto>> GetCategoriesAsync()
        {
            // Obtener las categorías del repositorio
            var categories = await _categoryRepository.GetCategoriesAsync();

            // Mapear toda la colección de una vez
            return _mapper.Map<ICollection<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetCategoryAsync(int id)
        {
            // Obtener la categoría del repositorio
            var category = await GetCategoryByIdAsync(id);

            // Mapear toda la colección de una vez
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> UpdateCategoryAsync(CategoryCreateDto dto, int id)
        {
            //Verificar si la categoría existe
            var existingCategory = await GetCategoryByIdAsync(id);

            var nameExists = await _categoryRepository.CategoryExistsByNameAsync(dto.Name);
            if (nameExists)
            {
                throw new InvalidOperationException($"Ya existe una categoría con el nombre de '{dto.Name}'");
            }

            //Mapear los cambios del DTO a la entidad existente Category
            _mapper.Map(dto, existingCategory);

            //Actualizar la categoría en el repositorio
            var isUpdated = await _categoryRepository.UpdateCategoryAsync(existingCategory);

            if (!isUpdated)
            {
                throw new Exception("Ocurrió un error al actualizar la categoría.");
            }

            //retornar el CategoryDto actualizado
            return _mapper.Map<CategoryDto>(existingCategory);
        }

        public Task<bool> UpdateCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }

        Task<ICollection<Category>> ICategoryService.GetCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        Task<Category> ICategoryService.GetCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        private async Task<Category> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryAsync(id);

            if (category == null)
            {
                throw new InvalidOperationException($"No se encontró la categoría con ID: '{id}'");
            }

            return category;
        }
    }
}

