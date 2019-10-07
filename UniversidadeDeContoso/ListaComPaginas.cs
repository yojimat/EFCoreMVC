using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UniversidadeDeContoso
{
    public class ListaComPaginas<T> : List<T>
    {
        public int IndexPagina { get; private set; }
        public int PaginaTotais { get; private set; }

        public ListaComPaginas(List<T> itens, int contador, int indexPagina, int tamanhoPagina)
        {
            IndexPagina = indexPagina;

            PaginaTotais = (int)Math.Ceiling(contador / (double)tamanhoPagina);

            AddRange(itens);
        }

        public bool TemPaginaAnterior => (IndexPagina > 1);
        public bool TemProximaPagina => (IndexPagina < PaginaTotais);

        public static async Task<ListaComPaginas<T>> CreateAsync(IQueryable<T> fonte, int indexPagina, int tamanhoPagina)
        {
            var contador = await fonte.CountAsync();

            var itens = await fonte.Skip((indexPagina - 1) * tamanhoPagina).Take(tamanhoPagina).ToListAsync();

            return new ListaComPaginas<T>(itens, contador, indexPagina, tamanhoPagina);
        }
    }
}
