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
        private TipoNewletter _tiponewletterDomain;
        private EmailNewletter _emailnewletterDomain;
        private EnvioNewletter _envionewletterDomain;
        private Acesso _acessoDomain;
        private TipoContato _tipocontatoDomain;
        private Contato _contatoDomain;
        private Menu _menuDomain;
        private MenuUsuario _menuusuarioDomain;
        private Inbox _inboxDomain;

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

        public TipoNewletter TipoNewletterDomain
        {
            get
            {
                if (this._tiponewletterDomain == null)
                {
                    this._tiponewletterDomain = new TipoNewletter();
                }

                return _tiponewletterDomain;
            }
        }

        public EmailNewletter EmailNewletterDomain
        {
            get
            {
                if (this._emailnewletterDomain == null)
                {
                    this._emailnewletterDomain = new EmailNewletter();
                }

                return _emailnewletterDomain;
            }
        }

        public EnvioNewletter EnvioNewletterDomain
        {
            get
            {
                if (this._envionewletterDomain == null)
                {
                    this._envionewletterDomain = new EnvioNewletter();
                }

                return _envionewletterDomain;
            }
        }

        public Acesso AcessoDomain
        {
            get
            {
                if (this._acessoDomain == null)
                {
                    this._acessoDomain = new Acesso();
                }

                return _acessoDomain;
            }
        }

        public TipoContato TipoContatoDomain
        {
            get
            {
                if (this._tipocontatoDomain == null)
                {
                    this._tipocontatoDomain = new TipoContato();
                }

                return _tipocontatoDomain;
            }
        }

        public Contato ContatoDomain
        {
            get
            {
                if (this._contatoDomain == null)
                {
                    this._contatoDomain = new Contato();
                }

                return _contatoDomain;
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

        public MenuUsuario MenuUsuarioDomain
        {
            get
            {
                if (this._menuusuarioDomain == null)
                {
                    this._menuusuarioDomain = new MenuUsuario();
                }

                return _menuusuarioDomain;
            }
        }

        public Inbox InboxDomain
        {
            get
            {
                if (this._inboxDomain == null)
                {
                    this._inboxDomain = new Inbox();
                }

                return _inboxDomain;
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
                    _tiponewletterDomain.Dispose();
                    _emailnewletterDomain.Dispose();
                    _envionewletterDomain.Dispose();
                    _acessoDomain.Dispose();
                    _tipocontatoDomain.Dispose();
                    _contatoDomain.Dispose();
                    _menuDomain.Dispose();
                    _menuusuarioDomain.Dispose();
                    _inboxDomain.Dispose();
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

