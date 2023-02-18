
import string
import random

from cls.City import City

def generate_cordinate(size_map):
        return random.randint(0, size_map)

def create_board(row, column):
    arr=[]
    rows, cols=row, column
    for i in range(rows):
        col = []
        for j in range(cols):
            col.append(1)
        arr.append(col)
    return arr

def are_cordinates_save(x, y, board):
    if board[x][y] == 1:
        return True
    return False

def pick_random_char(choices, city_names):
    rand_char = random.choice(choices)
    city_names.append(rand_char)
    choices = choices.replace(rand_char, '')
    return choices, rand_char

def generate_cordinates(size):
    """
    returns random x and y position
    """
    return generate_cordinate(size), generate_cordinate(size)

def generate_map(size_map, num_city, city_list, city_names, choices):
    board = create_board(size_map, size_map)
    while(num_city != 0):
        x, y = generate_cordinates(size_map-1)
        
        if are_cordinates_save(x, y, board):

            choices, rand_char = pick_random_char(choices, city_names)
            city = City(x, y, rand_char)
            board[x][y] = city
            num_city -= 1

            city_list.append(city)

    return board, city_list
            
def print_board(board):
      for i in range(0, SIZE_MAP):
            for j in range(0, SIZE_MAP):
                  print(board[i][j], end=" ")
            print("")
