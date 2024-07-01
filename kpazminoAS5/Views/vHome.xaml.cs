using kpazminoAS5.Modelos;

namespace kpazminoAS5.Views;

public partial class vHome : ContentPage
{
    private Persona selectedPerson;

    public vHome()
    {
        InitializeComponent();
        ListaPersonas.SelectionChanged += OnSelectionChanged;
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        selectedPerson = e.CurrentSelection.FirstOrDefault() as Persona;
    }

    private void btnInsertar_Clicked(object sender, EventArgs e)
    {
        status.Text = "";
        App.personrepo.AddNewPerson(txtNombre.Text);
        status.Text = App.personrepo.StatusMessage;
        ListaPersonas.ItemsSource = App.personrepo.GetAllPeople(); // Refresh list after insert
    }

    private void btnListar_Clicked(object sender, EventArgs e)
    {
        status.Text = "";
        List<Persona> people = App.personrepo.GetAllPeople();
        ListaPersonas.ItemsSource = people;
    }

    private void btnActualizar_Clicked(object sender, EventArgs e)
    {
        status.Text = "";
        if (selectedPerson != null)
        {
            selectedPerson.Name = txtNombre.Text;
            App.personrepo.UpdatePerson(selectedPerson);
            status.Text = App.personrepo.StatusMessage;
            ListaPersonas.ItemsSource = App.personrepo.GetAllPeople(); // Refresh list after update
        }
        else
        {
            status.Text = "Seleccione una persona para actualizar";
        }
    }

    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        status.Text = "";
        if (selectedPerson != null)
        {
            App.personrepo.DeletePerson(selectedPerson.Id);
            status.Text = App.personrepo.StatusMessage;
            ListaPersonas.ItemsSource = App.personrepo.GetAllPeople(); // Refresh list after delete
        }
        else
        {
            status.Text = "Seleccione una persona para eliminar";
        }
    }
}