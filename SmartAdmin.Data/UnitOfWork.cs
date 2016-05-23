using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartAdmin.Data.Model;
using SmartAdmin.Data.Repository;
using SmartAdmin.Data.ApplicationContext;

namespace SmartAdmin.Data
{
    public class UnitOfWork : IDisposable
    {
        private SmartAdminContext _Context = new SmartAdminContext();
        private bool _Disposed = false;

        private RepositoryGeneric<UsuarioDto> _usuarioRepository;
        private RepositoryGeneric<AcessoDto> _acessoRepository;
        private RepositoryGeneric<MenuDto> _menuRepository;
        private RepositoryGeneric<MenuUsuarioDto> _menuusuarioRepository;

        public RepositoryGeneric<UsuarioDto> UsuarioRepository
        {
            get
            {
                if (this._usuarioRepository == null)
                {
                    this._usuarioRepository = new RepositoryGeneric<UsuarioDto>(_Context);
                }
                return _usuarioRepository;
            }
        }

        public RepositoryGeneric<AcessoDto> AcessoRepository
        {
            get
            {
                if (this._acessoRepository == null)
                {
                    this._acessoRepository = new RepositoryGeneric<AcessoDto>(_Context);
                }
                return _acessoRepository;
            }
        }

        public RepositoryGeneric<MenuDto> MenuRepository
        {
            get
            {
                if (this._menuRepository == null)
                {
                    this._menuRepository = new RepositoryGeneric<MenuDto>(_Context);
                }
                return _menuRepository;
            }
        }

        public RepositoryGeneric<MenuUsuarioDto> MenuUsuarioRepository
        {
            get
            {
                if (this._menuusuarioRepository == null)
                {
                    this._menuusuarioRepository = new RepositoryGeneric<MenuUsuarioDto>(_Context);
                }
                return _menuusuarioRepository;
            }
        }

        public void Dispose()
        {
            Clear(true);
            GC.SuppressFinalize(this);
        }

        private void Clear(bool disposing)
        {
            if (!this._Disposed)
            {
                if (disposing)
                {
                    _Context.Dispose();
                    _usuarioRepository = null;
                    _acessoRepository = null;
                    _menuRepository = null;
                    _menuusuarioRepository = null;
                }
            }
            _Disposed = true;
        }

        ~UnitOfWork()
        {
            Clear(false);
        }
    }
}

