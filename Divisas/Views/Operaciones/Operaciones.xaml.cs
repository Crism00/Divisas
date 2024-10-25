using Divisas.Controllers;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace Divisas.Views
{
    public partial class Operaciones : ContentPage
    {
        private Picker? pickerDe;
        private Picker? pickerA;

        private string txtDe = "...";
        private string txtA = "...";
        private double valorCompra, valorVenta;
        private double cantidad;
        private double total;
        private bool isCompra = true;

        private Entry? cantidadEntry; 

        
        public Operaciones()
        {
            valorCompra = 0;
            valorVenta = 0;
            cantidad = 0;
            total = 0;
            pickerDe = new Picker();
            pickerA = new Picker();
            cantidadEntry = new Entry();
            InitializeComponent();
            // Inicializar con el contenido de Compra
            SetCompraContent();
        }

        private void limpiarDatos(){
            valorCompra = 0;
            valorVenta = 0;
            cantidad = 0;
            total = 0;
        }

        private void OnCompraClicked(object sender, EventArgs e)
        {
            isCompra = true;
            limpiarDatos();
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
            isCompra = false;
            limpiarDatos();
            SetVentaContent();

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

        //COMPONENTE COMPRA
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
                SelectedIndex = -1
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
                 SelectedIndex = -1
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

            cantidadEntry = new Entry
            {
                Text = "0.00",
                Keyboard = Keyboard.Numeric,
                HorizontalOptions = LayoutOptions.Fill,
                Margin = new Thickness(10, 0, 0, 0),
            };

            if (cantidadEntry != null)
            {
                cantidadEntry.TextChanged += OnCantidadChanged;
            }

            String txtbtn = "Realizar Venta";
            if(isCompra){
                txtbtn = "Realizar Compra";
            }

            var button = new Button
            {
                Text = txtbtn,
                BackgroundColor = Color.FromArgb("#00CFC1"),
                TextColor = Colors.White,
                CornerRadius = 10,
                HeightRequest = 50,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Fill,
                Margin = new Thickness(0, 20, 0, 0)
            };

            // Asigna el método al evento Clicked
            button.Clicked += OnButtonClicked;

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
                                    cantidadEntry,
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
                                    new Entry 
                                    { 
                                        Text = valorCompra.ToString("N2"), // Aquí usamos el valor de 'valorCompra' y lo formateamos con dos decimales
                                        IsReadOnly = true, 
                                        HorizontalOptions = LayoutOptions.Fill, 
                                        Margin = new Thickness(10, 0, 0, 0) 
                                    },
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
                    button
                }
            };
        }

        //COMPONENTE VENTA
        private async void SetVentaContent()
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
                ItemsSource = monedaNombres,
                SelectedIndex = -1
            };
            pickerDe.SelectedIndexChanged += OnMonedaDeChanged;

            var stackLayout1 = new StackLayout
            {
                Children =
                {
                    new Label { Text = "De:", FontSize = 12, TextColor = Colors.Black },
                    pickerDe 
                }
            };
            Grid.SetColumn(stackLayout1, 0);

            pickerA = new Picker
            {
                ItemsSource = monedaNombres, 
                SelectedIndex = -1
            };
            pickerA.SelectedIndexChanged += OnMonedaAChanged;

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

            cantidadEntry = new Entry
            {
                Text = "0.00",
                Keyboard = Keyboard.Numeric,
                HorizontalOptions = LayoutOptions.Fill,
                Margin = new Thickness(10, 0, 0, 0),
            };

            cantidadEntry.TextChanged += OnCantidadChanged;

            String txtbtn = "Realizar Venta";
            if(isCompra){
                txtbtn = "Realizar Compra";
            } 

            var button = new Button
            {
                Text = txtbtn,
                BackgroundColor = Color.FromArgb("#00CFC1"),
                TextColor = Colors.White,
                CornerRadius = 10,
                HeightRequest = 50,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Fill,
                Margin = new Thickness(0, 20, 0, 0)
            };

            // Asigna el método al evento Clicked
            button.Clicked += OnButtonClicked;

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
                                    cantidadEntry,
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
                                    new Entry 
                                    { 
                                        Text = valorCompra.ToString("N2"), // Aquí usamos el valor de 'valorCompra' y lo formateamos con dos decimales
                                        IsReadOnly = true, 
                                        HorizontalOptions = LayoutOptions.Fill, 
                                        Margin = new Thickness(10, 0, 0, 0) 
                                    },
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
                    button
                }
            };
        }

        private async void OnMonedaDeChanged(object? sender, EventArgs e)
        {
            var picker = (Picker)sender;
            if (picker.SelectedIndex != -1)
            {
                string selectedMoneda = picker.Items[picker.SelectedIndex];
                picker.Title = selectedMoneda;
                txtDe = selectedMoneda;

                // Si la moneda seleccionada en 'De' no es MXN, asegurarse de que en 'A' esté MXN
                if (selectedMoneda != "MXN" && pickerA?.SelectedItem?.ToString() != "MXN")
                {
                    pickerA.SelectedIndex = pickerA.Items.IndexOf("MXN");
                }

                // Obtener los valores de compra y venta de la moneda seleccionada
                var monedaController = new MonedasController();
                var moneda = await monedaController.GetMonedaByNameAsync(selectedMoneda);

                if (moneda != null)
                {
                    if(selectedMoneda != "MXN"){
                        valorCompra = moneda.V_Compra;
                        valorVenta = moneda.V_Venta;
                        cantidad = 1;
                    }
                    
                    ActualizarVista();
                }
            }
        }


        private async void OnMonedaAChanged(object? sender, EventArgs e)
        {
            var picker = (Picker)sender!;
            if (picker.SelectedIndex != -1)
            {
                string selectedMoneda = picker.Items[picker.SelectedIndex];
                picker.Title = selectedMoneda;
                txtA = selectedMoneda;

                // Si la moneda seleccionada en 'A' no es MXN, asegurarse de que en 'De' esté MXN
                if (selectedMoneda != "MXN" && pickerDe?.SelectedItem?.ToString() != "MXN")
                {
                    pickerDe.SelectedIndex = pickerDe.Items.IndexOf("MXN");
                }  

                // Obtener los valores de compra y venta de la moneda seleccionada
                var monedaController = new MonedasController();
                var moneda = await monedaController.GetMonedaByNameAsync(selectedMoneda);

                if (moneda != null)
                {
                    if(selectedMoneda != "MXN"){
                        valorCompra = moneda.V_Compra;
                        valorVenta = moneda.V_Venta;
                        cantidad = 1;
                    }
                    
                    ActualizarVista();
                }
            }
        }

        private void ActualizarVista()
        {
            txtDe = pickerDe?.Title ?? "...";
            txtA = pickerA?.Title ?? "...";
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

                            var grid = stack.Children.OfType<Grid>().FirstOrDefault();
                            var cambioEntry = grid?.Children.OfType<Entry>().FirstOrDefault();  
                            if (cambioEntry != null)
                            {
                                cambioEntry.Text = cantidad.ToString("N2");  
                            }

                            var cantidadLabel = grid?.Children.OfType<Label>().LastOrDefault();
                            if (cantidadLabel != null)
                            {
                                cantidadLabel.Text = txtDe;
                            }
                        }

                        else if (label.Text.Contains("Tipo de Cambio"))
                        {
                            var grid = stack.Children.OfType<Grid>().FirstOrDefault();
                            var cambioEntry = grid?.Children.OfType<Entry>().FirstOrDefault();  // Busca el Entry para el tipo de cambio
                            if (cambioEntry != null)
                            {
                                cambioEntry.Text = isCompra ? valorCompra.ToString("N2") : valorVenta.ToString("N2");
                            }

                            var cambioLabel = grid?.Children.OfType<Label>().LastOrDefault();
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

        private void OnCantidadChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(e.NewTextValue, out double cantidadCh))
            {
                cantidad = cantidadCh;  // Guardar la cantidad que se ha introducido
                total = isCompra ? cantidad * valorCompra : cantidad * valorVenta;

                ActualizarTotal();
            }
        }

        private void ActualizarTotal()
        {
            // Busca el StackLayout del total y actualiza su valor
            foreach (var child in ((StackLayout)ContentArea.Content).Children)
            {
                if (child is StackLayout stack)
                {
                    var label = stack.Children.OfType<Label>().FirstOrDefault();
                    if (label != null && label.Text.Contains("Total"))
                    {
                        var totalEntry = stack.Children.OfType<Grid>().FirstOrDefault()?.Children.OfType<Entry>().FirstOrDefault();
                        if (totalEntry != null)
                        {
                            totalEntry.Text = total.ToString("N2");  // Actualiza el total formateado
                        }
                    }
                }
            }
        }

        private async void OnButtonClicked(object? sender, EventArgs e)
        {
            if(total <= 0){
                var toast = Toast.Make("Complete todos los datos.", ToastDuration.Short, 14);
                await toast.Show();
            } else{
                String txtMsg = isCompra ? "Compra realizada con éxito." : "Venta realizada con éxito.";
                var toast = Toast.Make(txtMsg, ToastDuration.Short, 14);
                await toast.Show();
                limpiarDatos();

                if(isCompra){
                    SetCompraContent();
                } else{
                    SetVentaContent();
                }
                
            }
        }

    }
}