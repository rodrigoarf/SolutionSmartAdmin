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
        private Repository<TipoNewletterDto> _tiponewletterRepository;
        private Repository<EmailNewletterDto> _emailnewletterRepository;
        private Repository<EnvioNewletterDto> _envionewletterRepository;
        private Repository<AcessoDto> _acessoRepository;
        private Repository<TipoContatoDto> _tipocontatoRepository;
        private Repository<ContatoDto> _contatoRepository;
        private Repository<MenuDto> _menuRepository;
        private Repository<MenuUsuarioDto> _menuusuarioRepository;
        private Repository<InboxDto> _inboxRepository;
        private Repository<BancoDto> _bancoRepository;
        private Repository<CedenteDto> _cedenteRepository;
        private Repository<MensagemDto> _mensagemRepository;
        private Repository<MensagemEnviadaDto> _mensagemenviadaRepository;

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

        public Repository<TipoNewletterDto> TipoNewletterRepository
        {
            get
            {
                if (this._tiponewletterRepository == null)
                {
                    this._tiponewletterRepository = new Repository<TipoNewletterDto>(_context);
                }

                return _tiponewletterRepository;
            }
        }

        public Repository<EmailNewletterDto> EmailNewletterRepository
        {
            get
            {
                if (this._emailnewletterRepository == null)
                {
                    this._emailnewletterRepository = new Repository<EmailNewletterDto>(_context);
                }

                return _emailnewletterRepository;
            }
        }

        public Repository<EnvioNewletterDto> EnvioNewletterRepository
        {
            get
            {
                if (this._envionewletterRepository == null)
                {
                    this._envionewletterRepository = new Repository<EnvioNewletterDto>(_context);
                }

                return _envionewletterRepository;
            }
        }

        public Repository<AcessoDto> AcessoRepository
        {
            get
            {
                if (this._acessoRepository == null)
                {
                    this._acessoRepository = new Repository<AcessoDto>(_context);
                }

                return _acessoRepository;
            }
        }

        public Repository<TipoContatoDto> TipoContatoRepository
        {
            get
            {
                if (this._tipocontatoRepository == null)
                {
                    this._tipocontatoRepository = new Repository<TipoContatoDto>(_context);
                }

                return _tipocontatoRepository;
            }
        }

        public Repository<ContatoDto> ContatoRepository
        {
            get
            {
                if (this._contatoRepository == null)
                {
                    this._contatoRepository = new Repository<ContatoDto>(_context);
                }

                return _contatoRepository;
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

        public Repository<MenuUsuarioDto> MenuUsuarioRepository
        {
            get
            {
                if (this._menuusuarioRepository == null)
                {
                    this._menuusuarioRepository = new Repository<MenuUsuarioDto>(_context);
                }

                return _menuusuarioRepository;
            }
        }

        public Repository<InboxDto> InboxRepository
        {
            get
            {
                if (this._inboxRepository == null)
                {
                    this._inboxRepository = new Repository<InboxDto>(_context);
                }

                return _inboxRepository;
            }
        }

        public Repository<BancoDto> BancoRepository
        {
            get
            {
                if (this._bancoRepository == null)
                {
                    this._bancoRepository = new Repository<BancoDto>(_context);
                }

                return _bancoRepository;
            }
        }

        public Repository<CedenteDto> CedenteRepository
        {
            get
            {
                if (this._cedenteRepository == null)
                {
                    this._cedenteRepository = new Repository<CedenteDto>(_context);
                }

                return _cedenteRepository;
            }
        }

        public Repository<MensagemDto> MensagemRepository
        {
            get
            {
                if (this._mensagemRepository == null)
                {
                    this._mensagemRepository = new Repository<MensagemDto>(_context);
                }

                return _mensagemRepository;
            }
        }

        public Repository<MensagemEnviadaDto> MensagemEnviadaRepository
        {
            get
            {
                if (this._mensagemenviadaRepository == null)
                {
                    this._mensagemenviadaRepository = new Repository<MensagemEnviadaDto>(_context);
                }

                return _mensagemenviadaRepository;
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
                    _tiponewletterRepository = null;
                    _emailnewletterRepository = null;
                    _envionewletterRepository = null;
                    _acessoRepository = null;
                    _tipocontatoRepository = null;
                    _contatoRepository = null;
                    _menuRepository = null;
                    _menuusuarioRepository = null;
                    _inboxRepository = null;
                    _bancoRepository = null;
                    _cedenteRepository = null;
                    _mensagemRepository = null;
                    _mensagemenviadaRepository = null;
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

