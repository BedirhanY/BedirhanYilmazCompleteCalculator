using System;
using System.Globalization;

namespace Calculator;

public partial class MainPage : ContentPage
{
    private double firstNumber = 0;
    private double secondNumber = 0;
    private string currentOperator = ""; // + - * / =
    private bool isFirstNumberAfterOperator = true;

    public MainPage()
    {
        InitializeComponent();
    }

    private double ReadDisplay()
    {
        if (string.IsNullOrWhiteSpace(Display.Text))
        {
            return 0;
        }

        if (double.TryParse(Display.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
        {
            return value;
        }

        return 0;
    }

    private void WriteDisplay(double value)
    {
        Display.Text = value.ToString(CultureInfo.InvariantCulture);
    }

    private void OnNumberPressed(object? sender, EventArgs e)
    {
        var pressedButton = sender as Button;
        if (pressedButton == null)
        {
            return;
        }

        if (isFirstNumberAfterOperator || Display.Text == "0")
        {
            Display.Text = pressedButton.Text;
            isFirstNumberAfterOperator = false;
        }
        else
        {
            Display.Text = Display.Text + pressedButton.Text;
        }
    }

    private void OnDecimalPointPressed(object? sender, EventArgs e)
    {
        if (isFirstNumberAfterOperator)
        {
            Display.Text = "0";
            isFirstNumberAfterOperator = false;
        }

        if (!Display.Text.Contains("."))
        {
            Display.Text = Display.Text + ".";
        }
    }

    private void OnOperatorPressed(object? sender, EventArgs e)
    {
        var pressedButton = sender as Button;
        if (pressedButton == null)
        {
            return;
        }

        if (isFirstNumberAfterOperator)
        {
            currentOperator = pressedButton.Text;
            return;
        }

        isFirstNumberAfterOperator = true;

        if (currentOperator == string.Empty)
        {
            currentOperator = pressedButton.Text;
            firstNumber = ReadDisplay();
        }
        else
        {
            secondNumber = ReadDisplay();
            double result = firstNumber;

            switch (currentOperator)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    break;
                case "-":
                    result = firstNumber - secondNumber;
                    break;
                case "*":
                    result = firstNumber * secondNumber;
                    break;
                case "/":
                    if (secondNumber != 0)
                    {
                        result = firstNumber / secondNumber;
                    }
                    break;
            }

            WriteDisplay(result);
            currentOperator = pressedButton.Text;
            if (pressedButton.Text == "=")
            {
                currentOperator = string.Empty;
            }

            firstNumber = result;
        }
    }

    private void OnClearAllPressed(object? sender, EventArgs e)
    {
        firstNumber = 0;
        secondNumber = 0;
        currentOperator = string.Empty;
        isFirstNumberAfterOperator = true;
        Display.Text = "0";
    }

    private void OnClearEntryPressed(object? sender, EventArgs e)
    {
        Display.Text = "0";
        isFirstNumberAfterOperator = true;
    }

    private void OnSqrtPressed(object? sender, EventArgs e)
    {
        double value = ReadDisplay();
        if (value < 0)
        {
            return;
        }

        double result = Math.Sqrt(value);
        WriteDisplay(result);
        isFirstNumberAfterOperator = true;
    }

    private void OnSquarePressed(object? sender, EventArgs e)
    {
        double value = ReadDisplay();
        double result = value * value;
        WriteDisplay(result);
        isFirstNumberAfterOperator = true;
    }
}
