using Microsoft.Maui.Controls;

namespace Divisas.Views
{
    public partial class Operaciones : ContentPage
    {
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

        private void SetCompraContent()
        {
            var grid = new Grid
            {
                Padding = new Thickness(0, 10),
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };

            var stackLayout1 = new StackLayout
            {
                Children =
                {
                    new Label { Text = "De:", FontSize = 12, TextColor = Colors.Black },
                    new Picker
                    {
                        Title = "USD",
                        ItemsSource = new string[] { "USD", "EUR", "MXN", "JPY" }
                    }
                }
            };
            Grid.SetColumn(stackLayout1, 0);

            var stackLayout2 = new StackLayout
            {
                Children =
                {
                    new Label { Text = "A:", FontSize = 12, TextColor = Colors.Black },
                    new Picker
                    {
                        Title = "MXN",
                        ItemsSource = new string[] { "USD", "EUR", "MXN", "JPY" }
                    }
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
                            new Label { Text = "Cantidad USD:", FontSize = 12, TextColor = Colors.Black },
                            new Grid
                            {
                                Children =
                                {
                                    new Label { Text = "$", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, TextColor = Colors.Gray },
                                    new Entry { Text = "0.00", Keyboard = Keyboard.Numeric, HorizontalOptions = LayoutOptions.Fill },
                                    new Label { Text = "USD", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.End, TextColor = Colors.Gray }
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
                                    new Entry { Text = "0.00", IsReadOnly = true, HorizontalOptions = LayoutOptions.Fill },
                                    new Label { Text = "USD/MXN", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.End, TextColor = Colors.Gray }
                                }
                            }
                        }
                    },
                    new StackLayout
                    {
                        Children =
                        {
                            new Label { Text = "Total MXN:", FontSize = 12, TextColor = Colors.Black },
                            new Grid
                            {
                                Children =
                                {
                                    new Label { Text = "$", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Start, TextColor = Colors.Gray },
                                    new Entry { Text = "0.00", IsReadOnly = true, HorizontalOptions = LayoutOptions.Fill },
                                    new Label { Text = "MXN", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.End, TextColor = Colors.Gray }
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
    }
}