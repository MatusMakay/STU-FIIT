
def is_move_save(new_x, new_y, chessboard, size):
    # pozicia musi byt v intervale [0..size-1] a zaroven policko musi byt nenavstivene
    if new_x > -1 and new_y > -1 and new_x < size and new_y < size and chessboard[new_x][new_y] == 0:
        return True
    return False

# geeks for geeks
def create_chessboard(row, column):
    arr=[]
    rows, cols=row, column
    for i in range(rows):
        col = []
        for j in range(cols):
            col.append(0)
        arr.append(col)
    return arr

def read_file(filename):
    f = open(filename)
    return f.read(5)

def convert(string):
    str_list = list(string.split(" "))
    num_list = []
    for s in str_list:
        num_list.append(int(s))
    return num_list