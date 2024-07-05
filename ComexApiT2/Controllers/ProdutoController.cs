﻿using AutoMapper;
using ComexApiT2.Data;
using ComexApiT2.Dtos.Produtos;
using ComexApiT2.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComexApiT2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private ComexContext _context;
        private IMapper _mapper;

        public ProdutoController(ComexContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionarProduto([FromBody] CreateProdutoDto produtoDto ) 
        {
            Produto produto = _mapper.Map<Produto>( produtoDto );

            _context.Produtos.Add( produto );
            _context.SaveChanges();
            return Ok("Produto Adicionado com sucesso!");
        }

        [HttpGet]
        public IActionResult ListarProdutos()
        {
            var produtos = _context.Produtos.ToList();
            return Ok(_mapper.Map<List<ReadProdutoDto>>(produtos));
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarProduto(int id, [FromBody] UpdateProdutoDto produtoDto)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);

            if (produto == null)
            {
                return NotFound("Produto Não Encontrado!");
            }

            _mapper.Map(produtoDto, produto);
            _context.SaveChanges();
            return Ok("Produto Atualizado com Sucesso!");
        }

        [HttpDelete("{id}")]
        public IActionResult ApagarProduto(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);

            if (produto == null)
            {
                return NotFound("Produto Não Encontrado!");
            }

            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            return Ok("Produto apagado com Sucesso!");
        }
    }
}
