﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using back_end.DTO;
using back_end.Services.Interfaces;
using Domain.Entidades;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace back_end.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFuncionarioRepository _repository;
        public FuncionarioService(IUnitOfWork unitOfWork, IMapper mapper,
            IFuncionarioRepository repository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Adicionar(FuncionarioAddDTO dto)
        {
            //var funcionario = new Funcionario
            //{
            //    Email = dto.Email,
            //    Nome = dto.Nome,
            //    CPF = dto.CPF,
            //    Login = dto.Login,
            //    Senha = dto.Senha
            //};

            //opcao com automapper
            var funcionario = _mapper.Map<Funcionario>(dto);

           // await _unitOfWork.Repository<Funcionario>().Adicionar(funcionario);

            await SaveAsync(funcionario, _unitOfWork.Repository<Funcionario>().Adicionar);

            await _unitOfWork.Save();
        }

        public async Task Atualizar(FuncionarioUpdateDTO dto)
        {
            var funcionario = await _unitOfWork.Repository<Funcionario>().ObterPorId(dto.Id);
            if (funcionario is null) throw new Exception("Funcionario", new Exception("Funcionario nao encontrado."));

            funcionario.Email = dto.Email;
            funcionario.Nome = dto.Nome;
            funcionario.CPF = dto.CPF;

           // await _unitOfWork.Repository<Funcionario>().Atualizar(funcionario);
            await SaveAsync(funcionario, _unitOfWork.Repository<Funcionario>().Atualizar);
            await _unitOfWork.Save();
        }

        public  async Task AtualizarLogin(FuncionarioAtualizarLoginDTO dto)
        {
            var funcionario = await _unitOfWork.Repository<Funcionario>().ObterPorId(dto.Id);
            if (funcionario is null) throw new Exception("Funcionario", new Exception("Funcionario nao encontrado."));

            funcionario.Senha = dto.Senha;
            funcionario.Login = dto.Login;

            await _unitOfWork.Repository<Funcionario>().Atualizar(funcionario);
            await _unitOfWork.Save();
        }

        public async  Task<FuncionarioDTO> Obter(int id)
        {
            var funcionario = await _unitOfWork.Repository<Funcionario>().ObterPorId(id);
            if (funcionario is null) throw new Exception("Funcionario", new Exception("Funcionario nao encontrado."));
            return  _mapper.Map<FuncionarioDTO>(funcionario);

        }

        public async Task<List<FuncionarioDTO>> ObterPorNome(string nome)
        {
            return  _mapper.Map<List<FuncionarioDTO>>(await _repository.ObterPorNome(nome));
        }

        public async Task<IEnumerable<FuncionarioDTO>> ObterTodos()
        {
            return await _unitOfWork.Repository<Funcionario>()
                 .Entities.ProjectTo<FuncionarioDTO>(_mapper.ConfigurationProvider).ToListAsync();

        }

        public async Task Remove(int id)
        {
            var funcionario = await _unitOfWork.Repository<Funcionario>().ObterPorId(id);
            if (funcionario is null) throw new Exception("Funcionario", new Exception("Funcionario nao encontrado."));

            await _unitOfWork.Repository<Funcionario>().Remover(funcionario);

            await _unitOfWork.Save();

        }

        protected async Task<bool> SaveAsync<T>(IEnumerable<T> dataList, Func<T, Task> repository)
        where T : Entity, new()
        {
            var isValid = true;
            foreach (var data in dataList)
            {
                try
                {
                    await repository(data);
                }
                catch (Exception ex)
                {

                    Log.Fatal(
                        ex,
                        "Houve um erro ao salvar na tabela {Tabela} com {Id}",
                        typeof(T).Name,
                        data?.Id);

                    isValid = false;
                }
            }
            return isValid;
        }

        protected async Task<bool> SaveAsync<T>(T data, Func<T, Task> repository)
        where T : class, new()
        {
            var isValid = false;

            try
            {
                await repository(data);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex,
                     "Houve um erro ao salvar na tabela {Tabela}",
                     typeof(T).Name);

                isValid = false;
            }

            return isValid;
        }
    }
}
