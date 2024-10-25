using Divisas.Controllers;

namespace Divisas.Views
{
    public partial class Operaciones : ContentPage
    {
        private Picker pickerDe;
        private Picker pickerA;

        private String txtDe = "MXN";
        private String txtA = "USD";
        
        public Operaciones()
        {
            InitializeComponent();
            // Inicializar con el contenido de Compra
            SetCompraContent();
        }

        private void OnCompraClicked(object sender, EventArgs e)
        {
            SetCompraContent();

            // Cambiar colores de los botones
            ((Button)sender).BackgroundColor = Color.FromArgb("#00CFC1");
            ((Button)sender).TextColor = Colors.White;
            ((Button)sender).FontAttributes = FontAttributes.Bold;

            // Cambiar el otro botón
            var ventaButton = (Button)((Grid)((Button)sender).Parent).Children[1];
            ventaButton.BackgroundColor = Color.FromArgb("#E8E8E8");
            ventaButton.TextColor = Colors.Black;
            ventaButton.FontAttributes = FontAttributes.None;
        }

        private void OnVentaClicked(object sender, EventArgs e)
        {
            ContentArea.Content = new Label
            {
                Text = "Contenido de Venta",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            // Cambiar colores de los botones
            ((Button)sender).BackgroundColor = Color.FromArgb("#00CFC1");
            ((Button)sender).TextColor = Colors.White;
            ((Button)sender).FontAttributes = FontAttributes.Bold;

            // Cambiar el otro botón
            var compraButton = (Button)((Grid)((Button)sender).Parent).Children[0];
            compraButton.BackgroundColor = Color.FromArgb("#E8E8E8");
            compraButton.TextColor = Colors.Black;
            compraButton.FontAttributes = FontAttributes.None;
        }

        private async void SetCompraContent()
        {
            var monedaRepository = new MonedasController();

            // Obtener las monedas desde el repositorio
            var monedas = await monedaRepository.GetMonedasAsync();
            var monedaNombres = monedas.Select(m => m.Nombre).ToList();
            
            var grid = new Grid
            {
                Padding = new Thickness(0, 10),
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };

            pickerDe = new Picker
            {
                ItemsSource = monedaNombres, // Cargar las monedas en el picker
                SelectedIndex = monedaNombres.IndexOf("MXN")
            };
            pickerDe.SelectedIndexChanged += OnMonedaDeChanged;

            var stackLayout1 = new StackLayout
            {
                Children =
                {
                    new Label { Text = "De:", FontSize = 12, TextColor = Colors.Black },
                    pickerDe // Agregar el picker con su manejador
                }
            };
            Grid.SetColumn(stackLayout1, 0);

            pickerA = new Picker
            {
                ItemsSource = monedaNombres, // Cargar las monedas en el picker
                 SelectedIndex = monedaNombres.IndexOf("USD")
            };
            pickerA.SelectedIndexChanged += OnMonedaAChanged; // Agregamos el manejador del evento

            var stackLayout2 = new StackLayout
            {
                Children =
                {
                    new Label { Text = "A:", FontSize = 12, TextColor = Colors.Black },
                    pickerA // Agregar el picker con su manejador
                }
            };
            Grid.SetColumn(stackLayout2, 1);


            grid.Children.Add(stackLayout1);
            grid.Children.Add(stackLayout2);

            ContentArea.Content = new StackLayout
            {
                Spacing = 10,
                Children =
                {
                    grid,
                    new StackLayout
                    {
                        Children =
                        {
                            new Label { Text = "Cantidad " + txtDe + ":", FontSize = 12, TextColor = Colors.Black },
                            new Grid
                            {
                                Children =
                                {
                                    new Label { Text = "$", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, TextColor = Colors.Gray },
                                    new Entry { Text = "0.00", Keyboard = Keyboard.Numeric, HorizontalOptions = LayoutOptions.Fill, Margin = new Thickness(10, 0, 0, 0) },
                                    new Label { Text = txtDe, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.End, TextColor = Colors.Gray }
                                }
                            }
                        }
                    },
                    new StackLayout
                    {
                        Children =
                        {
                            new Label { Text = "Tipo de Cambio:", FontSize = 12, TextColor = Colors.Black },
                            new Grid
                            {
                                Children =
                                {
                                    new Label { Text = "$", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, TextColor = Colors.Gray },
                                    new Entry { Text = "0.00", IsReadOnly = true, HorizontalOptions = LayoutOptions.Fill, Margin = new Thickness(10, 0, 0, 0) },
                                    new Label { Text = txtDe + "/" + txtA, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.End, TextColor = Colors.Gray }
                                }
                            }
                        }
                    },
                    new StackLayout
                    {
                        Children =
                        {
                            new Label { Text = "Total " + txtA + ":", FontSize = 12, TextColor = Colors.Black },
                            new Grid
                            {
                                Children =
                                {
                                    new Label { Text = "$", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, TextColor = Colors.Gray },
                                    new Entry { Text = "0.00", IsReadOnly = true, HorizontalOptions = LayoutOptions.Fill, Margin = new Thickness(10, 0, 0, 0) },
                                    new Label { Text = txtA, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.End, TextColor = Colors.Gray }
                                }
                            }
                        }
                    },
                    new Button
                    {
                        Text = "Realizar Compra",
                        BackgroundColor = Color.FromArgb("#00CFC1"),
                        TextColor = Colors.White,
                        CornerRadius = 10,
                        HeightRequest = 50,
                        FontAttributes = FontAttributes.Bold,
                        HorizontalOptions = LayoutOptions.Fill,
                        Margin = new Thickness(0, 20, 0, 0)
                    }
                }
            };
        }

        private void OnMonedaDeChanged(object? sender, EventArgs e)
        {
            var picker = (Picker)sender!;
            if (picker.SelectedIndex != -1)
            {
                string selectedMoneda = picker.Items[picker.SelectedIndex];
                picker.Title = selectedMoneda;
                txtDe = selectedMoneda;

                // Si la moneda seleccionada en 'De' no es MXN, asegurarse de que en 'A' esté MXN
                if (selectedMoneda != "MXN" && pickerA.SelectedItem?.ToString() != "MXN")
                {
                    pickerA.SelectedIndex = pickerA.Items.IndexOf("MXN");
                }

                ActualizarVista();

                Console.WriteLine($"Moneda seleccionada DE: {selectedMoneda}");
            }
        }

        private void OnMonedaAChanged(object? sender, EventArgs e)
        {
            var picker = (Picker)sender!;
            if (picker.SelectedIndex != -1)
            {
                string selectedMoneda = picker.Items[picker.SelectedIndex];
                picker.Title = selectedMoneda;
                txtA = selectedMoneda;

                // Si la moneda seleccionada en 'A' no es MXN, asegurarse de que en 'De' esté MXN
                if (selectedMoneda != "MXN" && pickerDe.SelectedItem?.ToString() != "MXN")
                {
                    pickerDe.SelectedIndex = pickerDe.Items.IndexOf("MXN");
                }  

                ActualizarVista();

                Console.WriteLine($"Moneda seleccionada A: {selectedMoneda}");
            }
        }

        private void ActualizarVista()
        {
            // Actualiza los textos en la vista con los nuevos valores de txtDe y txtA
            // Puedes acceder a los elementos dentro del ContentArea y actualizar el contenido

            txtDe = pickerDe.Title;
            txtA = pickerA.Title;
            foreach (var child in ((StackLayout)ContentArea.Content).Children)
            {
                if (child is StackLayout stack)
                {
                    var label = stack.Children.OfType<Label>().FirstOrDefault();
                    if (label != null)
                    {
                        if (label.Text.Contains("Cantidad"))
                        {
                            label.Text = "Cantidad " + txtDe + ":";

                            var cantidadLabel = stack.Children.OfType<Grid>().FirstOrDefault()?.Children.OfType<Label>().LastOrDefault();
                            if (cantidadLabel != null)
                            {
                                cantidadLabel.Text = txtDe;
                            }
                        }
                        else if (label.Text.Contains("Tipo de Cambio"))
                        {
                            var cambioLabel = stack.Children.OfType<Grid>().FirstOrDefault()?.Children.OfType<Label>().LastOrDefault();
                            if (cambioLabel != null)
                            {
                                cambioLabel.Text = txtDe + "/" + txtA;
                            }
                        }
                        else if (label.Text.Contains("Total"))
                        {
                            label.Text = "Total " + txtA + ":";

                            var totalLabel = stack.Children.OfType<Grid>().FirstOrDefault()?.Children.OfType<Label>().LastOrDefault();
                            if (totalLabel != null)
                            {
                                totalLabel.Text = txtA;
                            }
                        }
                    }
                }
            }
        }



    }
}