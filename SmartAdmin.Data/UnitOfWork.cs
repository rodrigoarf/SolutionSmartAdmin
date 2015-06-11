using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartAdmin.Dto;
using SmartAdmin.Data.Generic;
using SmartAdmin.Data.Context;

namespace SmartAdmin.Data
{
    public class UnitOfWork : IDisposable
    {
        private SmartAdminContext _context = new SmartAdminContext();
        private bool _disposed = false;

        private Repository<UsuarioDto> _usuarioRepository;
        private Repository<MenuDto> _menuRepository;
        private Repository<PermissaoDto> _permissaoRepository;
        private Repository<ComplexidadeDto> _complexidadeRepository;
        private Repository<TipoComplexidadeDto> _tipocomplexidadeRepository;

        public Repository<UsuarioDto> UsuarioRepository
        {
            get
            {
                if (this._usuarioRepository == null)
                {
                    this._usuarioRepository = new Repository<UsuarioDto>(_context);
                }

                return _usuarioRepository;
            }
        }

        public Repository<MenuDto> MenuRepository
        {
            get
            {
                if (this._menuRepository == null)
                {
                    this._menuRepository = new Repository<MenuDto>(_context);
                }

                return _menuRepository;
            }
        }

        public Repository<PermissaoDto> PermissaoRepository
        {
            get
            {
                if (this._permissaoRepository == null)
                {
                    this._permissaoRepository = new Repository<PermissaoDto>(_context);
                }

                return _permissaoRepository;
            }
        }

        public Repository<ComplexidadeDto> ComplexidadeRepository
        {
            get
            {
                if (this._complexidadeRepository == null)
                {
                    this._complexidadeRepository = new Repository<ComplexidadeDto>(_context);
                }

                return _complexidadeRepository;
            }
        }

        public Repository<TipoComplexidadeDto> TipoComplexidadeRepository
        {
            get
            {
                if (this._tipocomplexidadeRepository == null)
                {
                    this._tipocomplexidadeRepository = new Repository<TipoComplexidadeDto>(_context);
                }

                return _tipocomplexidadeRepository;
            }
        }

        public void Dispose()
        {
            Clear(true);
            GC.SuppressFinalize(this);
        }

        private void Clear(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                    _usuarioRepository = null;
                    _menuRepository = null;
                    _permissaoRepository = null;
                    _complexidadeRepository = null;
                    _tipocomplexidadeRepository = null;
                }
            }
            _disposed = true;
        }

        ~UnitOfWork()
        {
            Clear(false);
        }
    }
}

