using System;

namespace Lab4 {

  class Program {

    public static void Main() {
      char[] numbers = { '1', '2', '3', '4', '5', '6', '7', '8' };
      char[] letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };

      System.Console.WriteLine("Выберите первую шахматную фигуру (конь(1), слон(2), ладья(3), ферзь(4))");
      int firstFigure = Convert.ToInt32(Console.ReadLine());
      System.Console.WriteLine("Выберите вторую шахматную фигуру (конь(1), слон(2), ладья(3), ферзь(4))");
      int secondFigure = Convert.ToInt32(Console.ReadLine());

      System.Console.WriteLine("Введите первое шахматное поле: ");
      string first = "" + Console.ReadLine();
      System.Console.WriteLine("Введите второе шахматное поле: ");
      string second = "" + Console.ReadLine();

      System.Console.WriteLine(Color(first));
      System.Console.WriteLine(Color(second));

      if (Danger(firstFigure, secondFigure, first, second, numbers, letters)) {
        System.Console.WriteLine("Первая фигура угрожает второй фигуре.");
      } else {
        System.Console.WriteLine("Первая фигура не угрожает второй фигуре.");
        System.Console.WriteLine("Поле, на которое нужно переместить первую фигуру, чтобы угрожать второй: " + FindThreatPosition(firstFigure, first, second, numbers, letters));
      }
    }

    public static string Color(string field) {
      char[] evenNumbers = { '2', '4', '6', '8' };
      char[] evenLetters = { 'B', 'D', 'F', 'H' };
      char[] oddNumbers = { '1', '3', '5', '7' };
      char[] oddLetters = { 'A', 'C', 'E', 'G' };
      char letterToFind = field[0];
      char numberToFind = field[1];

      if ((Array.Exists(evenLetters, element => element == letterToFind) && Array.Exists(evenNumbers, element => element == numberToFind)) ||
          (Array.Exists(oddLetters, element => element == letterToFind) && Array.Exists(oddNumbers, element => element == numberToFind))) {
        return "Поле " + field + " белое";
      } else {
        return "Поле " + field + " чёрное";
      }
    }

    public static bool Danger(int figure1, int figure2, string field1, string field2, char[] numbers, char[] letters) {
      char l1 = field1[0];
      char n1 = field1[1];
      char l2 = field2[0];
      char n2 = field2[1];

      if (figure1 == 1) {
        if ((Math.Abs(Array.IndexOf(numbers, n1) - Array.IndexOf(numbers, n2)) == 2 &&
            Math.Abs(Array.IndexOf(letters, l1) - Array.IndexOf(letters, l2)) == 1) ||
            (Math.Abs(Array.IndexOf(numbers, n1) - Array.IndexOf(numbers, n2)) == 1 &&
            Math.Abs(Array.IndexOf(letters, l1) - Array.IndexOf(letters, l2)) == 2)) {
          return true;
        }
      } else if (figure1 == 2) {
        if (Math.Abs(Array.IndexOf(numbers, n1) - Array.IndexOf(numbers, n2)) == Math.Abs(Array.IndexOf(letters, l1) - Array.IndexOf(letters, l2))) {
          return true;
        }
      } else if (figure1 == 3) {
        if (l1 == l2 || n1 == n2) {
          return true;
        }
      } else if (figure1 == 4) {
        if (Math.Abs(Array.IndexOf(numbers, n1) - Array.IndexOf(numbers, n2)) == Math.Abs(Array.IndexOf(letters, l1) - Array.IndexOf(letters, l2)) ||
            l1 == l2 || n1 == n2) {
          return true;
        }
      }

      return false;
    }

    public static string FindThreatPosition(int figure, string field1, string field2, char[] numbers, char[] letters) {
    char l1 = field1[0];
    char n1 = field1[1];
    char l2 = field2[0];
    char n2 = field2[1];

    int indexL1 = Array.IndexOf(letters, l1);
    int indexN1 = Array.IndexOf(numbers, n1);
    int indexL2 = Array.IndexOf(letters, l2);
    int indexN2 = Array.IndexOf(numbers, n2);
    
    if (figure == 1) {
        int[] knightMovesX = { 2, 2, -2, -2, 1, 1, -1, -1 };
        int[] knightMovesY = { 1, -1, 1, -1, 2, -2, 2, -2 };

        for (int i = 0; i < knightMovesX.Length; i++) {
            int newX = indexL2 + knightMovesX[i];
            int newY = indexN2 + knightMovesY[i];

            if (newX >= 0 && newX < letters.Length && newY >= 0 && newY < numbers.Length) {
                if ((Math.Abs(indexN1 - newY) == 2 && Math.Abs(indexL1 - newX) == 1) ||
                    (Math.Abs(indexN1 - newY) == 1 && Math.Abs(indexL1 - newX) == 2)) {
                    return letters[newX] + "" + numbers[newY];
                }
            }
        }
    }

    if (figure == 3 || figure == 4) {
        for (int i = 0; i < numbers.Length; i++) {
            if (i != indexN1) {
                if (l1 == l2 || numbers[i] == n2) {
                    return l1 + "" + numbers[i];
                }
            }
        }

        for (int i = 0; i < letters.Length; i++) {
            if (i != indexL1) {
                if (letters[i] == l2 || n1 == n2) {
                    return letters[i] + "" + n1;
                }
            }
        }
    }

    if (figure == 2 || figure == 4) {
        for (int i = 1; i < 8; i++) {
            if (indexL1 + i < letters.Length && indexN1 + i < numbers.Length) {
                char newLetter = letters[indexL1 + i];
                char newNumber = numbers[indexN1 + i];
                if (newLetter == l2 && newNumber == n2) return newLetter + "" + newNumber;
                if (Math.Abs(Array.IndexOf(letters, newLetter) - indexL2) ==
                    Math.Abs(Array.IndexOf(numbers, newNumber) - indexN2)) {
                    return newLetter + "" + newNumber;
                }
            }

            if (indexL1 - i >= 0 && indexN1 + i < numbers.Length) {
                char newLetter = letters[indexL1 - i];
                char newNumber = numbers[indexN1 + i];
                if (newLetter == l2 && newNumber == n2) return newLetter + "" + newNumber;
                if (Math.Abs(Array.IndexOf(letters, newLetter) - indexL2) ==
                    Math.Abs(Array.IndexOf(numbers, newNumber) - indexN2)) {
                    return newLetter + "" + newNumber;
                }
            }

            if (indexL1 + i < letters.Length && indexN1 - i >= 0) {
                char newLetter = letters[indexL1 + i];
                char newNumber = numbers[indexN1 - i];
                if (newLetter == l2 && newNumber == n2) return newLetter + "" + newNumber;
                if (Math.Abs(Array.IndexOf(letters, newLetter) - indexL2) ==
                    Math.Abs(Array.IndexOf(numbers, newNumber) - indexN2)) {
                    return newLetter + "" + newNumber;
                }
            }

            if (indexL1 - i >= 0 && indexN1 - i >= 0) {
                char newLetter = letters[indexL1 - i];
                char newNumber = numbers[indexN1 - i];
                if (newLetter == l2 && newNumber == n2) return newLetter + "" + newNumber;
                if (Math.Abs(Array.IndexOf(letters, newLetter) - indexL2) ==
                    Math.Abs(Array.IndexOf(numbers, newNumber) - indexN2)) {
                    return newLetter + "" + newNumber;
                }
            }
        }
    }

    return "Нет доступного поля для угрозы.";
    }
  }
}
