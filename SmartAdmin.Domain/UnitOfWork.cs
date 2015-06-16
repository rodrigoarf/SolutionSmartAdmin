using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdmin.Domain
{
    public class UnitOfWork : IDisposable
    {
        private Usuario _usuarioDomain;
        private Menu _menuDomain;
        private Permissao _permissaoDomain;
        private Complexidade _complexidadeDomain;
        private TipoComplexidade _tipocomplexidadeDomain;

        public Usuario UsuarioDomain
        {
            get
            {
                if (this._usuarioDomain == null)
                {
                    this._usuarioDomain = new Usuario();
                }

                return _usuarioDomain;
            }
        }

        public Menu MenuDomain
        {
            get
            {
                if (this._menuDomain == null)
                {
                    this._menuDomain = new Menu();
                }

                return _menuDomain;
            }
        }

        public Permissao PermissaoDomain
        {
            get
            {
                if (this._permissaoDomain == null)
                {
                    this._permissaoDomain = new Permissao();
                }

                return _permissaoDomain;
            }
        }

        public Complexidade ComplexidadeDomain
        {
            get
            {
                if (this._complexidadeDomain == null)
                {
                    this._complexidadeDomain = new Complexidade();
                }

                return _complexidadeDomain;
            }
        }

        public TipoComplexidade TipoComplexidadeDomain
        {
            get
            {
                if (this._tipocomplexidadeDomain == null)
                {
                    this._tipocomplexidadeDomain = new TipoComplexidade();
                }

                return _tipocomplexidadeDomain;
            }
        }

        private bool _disposed = false;

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
                    _usuarioDomain.Dispose();
                    _menuDomain.Dispose();
                    _permissaoDomain.Dispose();
                    _complexidadeDomain.Dispose();
                    _tipocomplexidadeDomain.Dispose();
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

