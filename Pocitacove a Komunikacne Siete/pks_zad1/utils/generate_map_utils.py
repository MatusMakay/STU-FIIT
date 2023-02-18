import random
from classes.cluster import Cluster


def create_board_fill_points(size_map, max_points, CLUSTER_TYPE, list_clusters):
    board=[]
    rows, cols = size_map, size_map
    for i in range(rows):
        col = []
        for j in range(cols):
            col.append(0)
        board.append(col)
    
    fill_map(size_map, max_points, CLUSTER_TYPE, board, list_clusters)

    return board
    


def generate_xy(max):
    x = random.randint(5, max - 5)
    y = random.randint(5, max - 5)

    return x,y

def calc_offset(coordinate, down_limit, upper_limit):
    down_offset, upper_offset = 0, 0

    if coordinate > upper_limit/2:
        # -100 moze +100 treba osetrit
        down_offset = -100
        decide = coordinate + 100

        if decide > upper_limit:
            upper_offset = upper_limit - coordinate
        
    else:
        upper_offset = 100
        
        decide = coordinate - 100

        if decide < 0: 
            #+100 moze -100 treba osetrit
            down_offset = down_limit - coordinate



    return down_offset, upper_offset

def are_cordinates_save(x, y, down_limit, upper_limit, board):
    if board[x][y] == 0 and x > down_limit and x < upper_limit and y > down_limit and y < upper_limit:
        return True
    return False


def pick_random_index(limit):
    return random.randint(0, limit - 1)

def generate_rest_points(max_points, down_limit, upper_limit, CLUSTER_TYPE, board, list_cluster):
    while(max_points >= 0):
        
        cordinates = list_cluster[pick_random_index(len(list_cluster))]
        x, y = cordinates[0], cordinates[1]
        
        x_down_offset, x_upper_offset = calc_offset(x, down_limit, upper_limit)
        y_down_offset, y_upper_offset = calc_offset(y, down_limit, upper_limit)

        x_offset = random.randint(x_down_offset, x_upper_offset) 
        y_offset = random.randint(y_down_offset, y_upper_offset) 

        while(are_cordinates_save((x+x_offset), (y+y_offset), down_limit, upper_limit, board) == False):
            x_offset = random.randint(x_down_offset, x_upper_offset)
            y_offset = random.randint(y_down_offset, y_upper_offset)
        
        cluster = Cluster(x + x_offset, y + y_offset, max_points, CLUSTER_TYPE)
        board[x + x_offset][y + y_offset] = cluster

        list_cluster.append(cluster.get_cordinates())
        max_points -= 1
    


def generate_first_points(size_map, max_points, down_limit, upper_limit, CLUSTER_TYPE, board, list_cluster):
    while max_points >= 0:
        
        x, y = generate_xy(size_map - 1)

        if are_cordinates_save(x, y, down_limit, upper_limit, board):
            cluster = Cluster(x, y, max_points, CLUSTER_TYPE)
        
            board[x][y] = cluster
            list_cluster.append(cluster.get_cordinates())
            
            max_points -= 1

    

def fill_map(size_map, max_points, CLUSTER_TYPE, board, list_cluster):
    UPPER_LIMIT = size_map - 50
    DOWN_LIMIT = 50

    #generate first 20 points
    generate_first_points(size_map, 20, 0, size_map, CLUSTER_TYPE, board, list_cluster)
    
    generate_rest_points(max_points - 22, DOWN_LIMIT, UPPER_LIMIT, CLUSTER_TYPE, board, list_cluster)

