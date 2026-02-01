using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using TrainingHub.Models;
using TrainingHub.Services;

namespace TrainingHub.ViewModels;

// ViewModel para a gestão de Cursos
// Herda de ViewModelBase para ter acesso a funcionalidades base (como INotifyPropertyChanged)
public partial class CoursesViewModel : ViewModelBase
{
    // Referências para o Model (Company) e Serviços
    private readonly Company _company;
    private readonly IDateProvider _dateProvider;

    // ===== Listas para a Interface Gráfica (UI) =====

    // Lista de cursos existentes para exibir na tabela/lista
    // ObservableCollection notifica a UI automaticamente quando itens são adicionados/removidos
    public ObservableCollection<Course> Courses { get; } = new();

    // Lista de treinadores disponíveis para preencher o ComboBox de seleção
    public ObservableCollection<Trainer> Trainers { get; } = new();


    // ===== Propriedades do Formulário de Novo Curso =====
    // O atributo [ObservableProperty] gera automaticamente o código necessário para notificar a UI de mudanças

    [ObservableProperty] private string newCourseName = string.Empty;

    [ObservableProperty] private string newCourseArea = string.Empty;

    // Usamos DateTimeOffset pois é o tipo preferido pelos controles de data do Avalonia (DatePicker)
    // Inicializamos com D+1 pois a regra de negócio exige datas futuras
    [ObservableProperty] private DateTimeOffset newStartDate;

    [ObservableProperty] private DateTimeOffset newEndDate;

    // Treinador selecionado no ComboBox
    [ObservableProperty] private Trainer? selectedNewTrainer;

    // Controla a visibilidade do modal de adição
    [ObservableProperty] private bool isAddingCourse;

    [ObservableProperty] private string errorMessage;


    // ===== Construtor =====
    // Recebe as dependências via Injeção de Dependência
    public CoursesViewModel(Company company, IDateProvider dateProvider)
    {
        _company = company;
        _dateProvider = dateProvider;

        // Define datas iniciais padrão (amanhã e daqui a 5 dias)
        NewStartDate = _dateProvider.Today.AddDays(1);
        NewEndDate = _dateProvider.Today.AddDays(5);

        // Carrega os dados iniciais
        LoadData();
    }

    // Método auxiliar para carregar dados do Model para as listas da ViewModel
    private void LoadData()
    {
        // 1. Carregar Cursos
        Courses.Clear();
        foreach (var course in _company.Courses)
        {
            Courses.Add(course);
        }

        // 2. Carregar Treinadores
        // Filtra a lista de empregados para pegar apenas os que são do tipo Trainer
        Trainers.Clear();
        foreach (var trainer in _company.Employees.OfType<Trainer>())
        {
            Trainers.Add(trainer);
        }
    }

    // ===== Comandos (Ações) =====

    // Abre o modal de adição
    [RelayCommand]
    private void OpenAddCourse()
    {
        ErrorMessage = string.Empty;
        // Limpa o formulário antes de abrir
        NewCourseName = string.Empty;
        NewCourseArea = string.Empty;
        SelectedNewTrainer = null;
        NewStartDate = _dateProvider.Today.AddDays(1);
        NewEndDate = _dateProvider.Today.AddDays(5);

        IsAddingCourse = true;
    }

    // Fecha o modal sem salvar
    [RelayCommand]
    private void CancelAddCourse()
    {
        IsAddingCourse = false;
    }

    // O atributo [RelayCommand] transforma este método em um ICommand que pode ser ligado a um botão
    [RelayCommand]
    private void SaveCourse()
    {
        ErrorMessage = string.Empty;

        // Validação 1: Campos obrigatórios
        if (string.IsNullOrWhiteSpace(NewCourseName) || SelectedNewTrainer == null)
        {
            ErrorMessage = "Please fill in the course name and select a trainer.";
            return; // Para aqui e não salva
        }

        // Validação 2: Data de Fim não pode ser menor que Data de Início
        if (NewEndDate.Date < NewStartDate.Date)
        {
            ErrorMessage = "End Date cannot be earlier than Start Date.";
            return; // Para aqui e não salva
        }

        // Tenta criar o curso
        try
        {
            var newCourse = new Course(
                NewCourseName,
                NewCourseArea,
                NewStartDate.DateTime,
                NewEndDate.DateTime,
                SelectedNewTrainer
            );

            // Se a Company lançar erro (ex: data no passado), capturamos aqui
            _company.AddCourse(newCourse);

            // Se chegou até aqui, deu tudo certo
            if (_company.Courses.Contains(newCourse))
            {
                Courses.Add(newCourse);
                IsAddingCourse = false; // Fecha o modal
            }
        }
        catch (ArgumentException ex)
        {
            // Mostra o erro que veio da classe Company ou Course
            ErrorMessage = ex.Message;
        }
    }
}
