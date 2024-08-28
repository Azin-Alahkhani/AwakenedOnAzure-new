using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniSudoku : MonoBehaviour
{
    public GameObject miniGamePanel;
    public TMP_InputField[] cells; // Change to TMP_InputField
    public Button submitButton;
    private int[,] solution = new int[4, 4]
    {
        {1, 2, 3, 4},
        {3, 4, 1, 2},
        {4, 3, 2, 1},
        {2, 1, 4, 3}
    };
    private int[,] currentPuzzle;

    private void Start()
    {
        miniGamePanel.SetActive(false);
        submitButton.onClick.AddListener(CheckSolution);
        ShuffleSolution();
        AddInputValidation(); // Add input validation
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowMiniGame();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HideMiniGame();
        }
    }

    private void ShuffleSolution()
    {
        // Add a simple shuffling algorithm to randomize the solution
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                int temp = solution[i, j];
                int randomRow = Random.Range(0, 4);
                int randomCol = Random.Range(0, 4);
                solution[i, j] = solution[randomRow, randomCol];
                solution[randomRow, randomCol] = temp;
            }
        }
    }

    public void ShowMiniGame()
    {
        miniGamePanel.SetActive(true);
        SetupNewPuzzle();
    }

    public void HideMiniGame()
    {
        miniGamePanel.SetActive(false); // Hide the mini-game panel
    }

    private void SetupNewPuzzle()
    {
        ShuffleSolution();
        currentPuzzle = (int[,])solution.Clone(); // Clone the solution for the current puzzle

        // Clear all cells
        for (int i = 0; i < cells.Length; i++)
        {
            cells[i].text = "";
            cells[i].interactable = true;
        }

        // Show a few cells as hints
        for (int i = 0; i < 6; i++) // Show 6 hints
        {
            int randomIndex = Random.Range(0, 16);
            int row = randomIndex / 4;
            int col = randomIndex % 4;
            cells[randomIndex].text = currentPuzzle[row, col].ToString();
            cells[randomIndex].interactable = false;
        }
    }

    public void CheckSolution()
    {
        int[,] playerGrid = new int[4, 4];

        // Parse player inputs
        for (int i = 0; i < 16; i++)
        {
            if (cells[i].interactable) // Only check input for editable cells
            {
                if (int.TryParse(cells[i].text, out int value))
                {
                    playerGrid[i / 4, i % 4] = value;
                }
                else
                {
                    Debug.Log("Invalid input in cell " + i);
                    return;
                }
            }
            else
            {
                playerGrid[i / 4, i % 4] = currentPuzzle[i / 4, i % 4];
            }
        }

        // Check if the player solution matches the shuffled solution
        if (IsCorrectSolution(playerGrid))
        {
            Debug.Log("Correct solution! Door opens.");
            OpenDoor();
        }
        else
        {
            Debug.Log("Incorrect solution. Try again.");
            SetupNewPuzzle(); // Show a new puzzle
        }
    }

    private bool IsCorrectSolution(int[,] playerGrid)
    {
        // Check rows and columns
        for (int i = 0; i < 4; i++)
        {
            bool[] rowCheck = new bool[4];
            bool[] colCheck = new bool[4];

            for (int j = 0; j < 4; j++)
            {
                if (playerGrid[i, j] < 1 || playerGrid[i, j] > 4 || rowCheck[playerGrid[i, j] - 1])
                    return false;

                if (playerGrid[j, i] < 1 || playerGrid[j, i] > 4 || colCheck[playerGrid[j, i] - 1])
                    return false;

                rowCheck[playerGrid[i, j] - 1] = true;
                colCheck[playerGrid[j, i] - 1] = true;
            }
        }

        return true;
    }

    private void OpenDoor()
    {
        // Implement the logic to open the door
        HideMiniGame();
        Debug.Log("Door is now open.");
        LevelManager.instance.OpenTheDoor();
    }

    private void AddInputValidation()
    {
        foreach (var cell in cells)
        {
            cell.characterValidation = TMP_InputField.CharacterValidation.CustomValidator;
            cell.onValidateInput += ValidateInput;
        }
    }

    private char ValidateInput(string text, int charIndex, char addedChar)
    {
        
        if (addedChar >= '1' && addedChar <= '4')
        {
            return addedChar;
        }
        return '\0'; // Invalid character
    }
}
