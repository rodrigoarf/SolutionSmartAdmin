using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartAdmin.Domain.Model;

namespace SmartAdmin.Domain
{
    public class UnitOfWork : IDisposable
    {
        private bool _Disposed = false;

        private Usuario _usuarioDomain;
        private Acesso _acessoDomain;
        private Menu _menuDomain;
        private MenuUsuario _menuusuarioDomain;

        public Usuario UsuarioDomain
        {
            get
            {
                if (this._usuarioDomain == null)
                {
                    this._usuarioDomain = new UsuarioSpecialized();
                }
                return _usuarioDomain;
            }
        }
        public Acesso AcessoDomain
        {
            get
            {
                if (this._acessoDomain == null)
                {
                    this._acessoDomain = new AcessoSpecialized();
                }
                return _acessoDomain;
            }
        }
        public Menu MenuDomain
        {
            get
            {
                if (this._menuDomain == null)
                {
                    this._menuDomain = new MenuSpecialized();
                }
                return _menuDomain;
            }
        }
        public MenuUsuario MenuUsuarioDomain
        {
            get
            {
                if (this._menuusuarioDomain == null)
                {
                    this._menuusuarioDomain = new MenuUsuarioSpecialized();
                }
                return _menuusuarioDomain;
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
                    _usuarioDomain = null;
                    _acessoDomain = null;
                    _menuDomain = null;
                    _menuusuarioDomain = null;
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

