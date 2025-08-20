using MauiAppFit.Models;
using System.ComponentModel;
using System.Windows.Input;


namespace MauiAppFit.ViewModels
{
    [QueryProperty("PegarIdNaNavegação", "parametro_id")]

    public class CadastroAtividadeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string descricao, observacoes;
        int id;
        DateTime data:
        double? peso;

        public string PegarIdNavegacao
        {
            set
            {
                int id_parametro = Convert.ToInt32(Uri.UnescapeDataString(value));

                VerAtividade.Execute(id_parametro);
            }
        }

        public string Descricao
        {
            get => descricao;
            set
            {
                descricao = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Descricao"));
            }
        }

        public string Observacoes
        {
            get => observacoes;
            set
            {
                observacoes = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Observacoes"));
            }
        }
        public int Id
        {
            get => id;
            set
            {
                id = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Id"));
            }
        }

        public DateTime Data
        {
            get => data;
            set
            {
                data = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Data"));
            }

        }

        public ICommand NovaAtividade
        {
            get => new Command(() =>
            {
                id = 0;
                Descricao = String.Empty;
                Data = DateTime.Now;
                peso = null;
                Observacao = String.Empty;
            });
        }

        public ICommand SalvarAtividade
        {
            get => new Command(async () =>
            {
                try
                {
                    Atividade model = new()
                    {
                        Descricao = this.Descricao,
                        Data = this.Data,
                        Peso = this.Peso,
                        Observacoes = this.Observacao

                    };

                    if (this.Id == 0)
                    {
                        await App.Database.Insert(model);
                    }
                    else
                    {
                        model.Id = this.Id;
                        await MauiAppFit.Database.Update(model);
                    }

                    await Shell.Current.DisplayAlert("Beleza", "Atividade salva!", "OK");

                    await Shell.Current.GoToAsync("//MinhaAtividades");
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Ops", ex.Message, "OK");
                }
            });
        }

        public ICommand VerAtividade
        {
            get => new Command<int>(async (int id) =>
            {
                try
                {
                    Atividade model = await App.Database.GetById(id);

                    this.Id = model.Id;
                    this.Descricao = model.Descricao;
                    this.peso = model.Peso;
                    this.Data = model.Data;
                    this.Observacoes = model.Observacoes;
                }

                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Ops", ex.Message, "OK");
                }

            });
        }
    }
}
