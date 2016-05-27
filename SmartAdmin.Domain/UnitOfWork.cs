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
        private NoticiaPublicador _noticiapublicadorDomain;
        private NoticiaCategoria _noticiacategoriaDomain;
        private Noticia _noticiaDomain;

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
        public NoticiaPublicador NoticiaPublicadorDomain
        {
            get
            {
                if (this._noticiapublicadorDomain == null)
                {
                    this._noticiapublicadorDomain = new NoticiaPublicadorSpecialized();
                }
                return _noticiapublicadorDomain;
            }
        }
        public NoticiaCategoria NoticiaCategoriaDomain
        {
            get
            {
                if (this._noticiacategoriaDomain == null)
                {
                    this._noticiacategoriaDomain = new NoticiaCategoriaSpecialized();
                }
                return _noticiacategoriaDomain;
            }
        }
        public Noticia NoticiaDomain
        {
            get
            {
                if (this._noticiaDomain == null)
                {
                    this._noticiaDomain = new NoticiaSpecialized();
                }
                return _noticiaDomain;
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
                    _noticiapublicadorDomain = null;
                    _noticiacategoriaDomain = null;
                    _noticiaDomain = null;
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

