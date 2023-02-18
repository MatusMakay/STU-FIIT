#!/usr/bin/python3

from utils.generate_map_utils import create_board_fill_points
from random import randint
import math
import sys

import matplotlib.pyplot as plt

# TODO interaktivne menenie type Cluster


if len(sys.argv) != 4:
    exit("Send args: max_points num_cluster M/C")
    
m = int(sys.argv[1])
n = int(sys.argv[2])
t = sys.argv[3]

MAX_SIZE_MAP = 10000

NUM_POINTS = m
NUM_KLUSTER = n

if t == "M" or t == "m" or t == "medoid" or t == "MEDOID":
    CLUSTER_TYPE = "MEDOID"

else:
    CLUSTER_TYPE = "CENTROID"


list_coordinates = []
board = create_board_fill_points(MAX_SIZE_MAP, NUM_POINTS, CLUSTER_TYPE, list_coordinates)

"""
MAIN CODE
"""

def join_clusters(source, destination):
    """
    Join clusters, append points from first argument to second argument
    Return source.xy, destination_old_xy, destination_new_xy 
    """
    old_dest_x, old_dest_y, new_dest_x, new_dest_y = destination.add_cluster_points(source.list_points)
    source_cord = source.get_cordinates()

    return source_cord[0], source_cord[1], old_dest_x, old_dest_y, new_dest_x, new_dest_y

while(len(list_coordinates) != NUM_KLUSTER):
    random_idx = randint(0, len(list_coordinates) - 1)
    
    pick_cordinates = list_coordinates[random_idx] 
    
    x, y = pick_cordinates[0], pick_cordinates[1]

    picked_cluster = board[x][y]

    min_cord=()

    i = 0
    min_cord = list_coordinates[i]

    #ak sa min_cord rovna tomu co som vylosoval posun sa dalej v poli a zmen min_cord
    while min_cord == pick_cordinates:
        i+=1
        min_cord = list_coordinates[i]

    min_dis = math.dist(pick_cordinates, min_cord)

    for cordinates in list_coordinates:
        if cordinates != pick_cordinates and cordinates != min_cord:
            euc_dis = math.dist(pick_cordinates, cordinates)
            if euc_dis < min_dis:
                min_dis = euc_dis
                min_cord = cordinates

    x, y = min_cord[0], min_cord[1]
    closest_cluster = board[x][y]

    final_cluster = {}

    if len(picked_cluster.list_points) > len(closest_cluster.list_points):
         source_x, source_y, old_dest_x, old_dest_y, new_dest_x, new_dest_y = join_clusters(closest_cluster, picked_cluster)
         final_cluster = picked_cluster
    else:
        source_x, source_y, old_dest_x, old_dest_y, new_dest_x, new_dest_y = join_clusters(picked_cluster, closest_cluster)
        final_cluster = closest_cluster
    
    board[source_x][source_y] = 0
    board[old_dest_x][old_dest_y] = 0

    if board[new_dest_x][new_dest_y] != 0 :
        print("ou no")

    board[new_dest_x][new_dest_y] = final_cluster

    list_coordinates.remove((source_x, source_y))
    
    if (source_x, source_y) != (old_dest_x, old_dest_y):
        list_coordinates.remove((old_dest_x, old_dest_y))

    list_coordinates.append((new_dest_x, new_dest_y))    

"""
CALCULATE ERROR
"""
def calc_error(list_coordinates):
    error_rate = 0
    for coordinate in list_coordinates:
        x, y = coordinate[0], coordinate[1]
        cluster = board[x][y]
        
        average_dis = cluster.calc_average_distance_from_centroid()

        if average_dis > 500:
            error_rate += 1        
            
    return error_rate

error_rate = calc_error(list_coordinates)

print(f"Error rate: {round(error_rate/len(list_coordinates)*100)}")

"""
GENERATE COLORS FOR CLUSTERS
"""


# generate colors from stack_overlow
def htmlcolor(r, g, b):
    def _chkarg(a):
        if isinstance(a, int): # clamp to range 0--255
            if a < 0:
                a = 0
            elif a > 255:
                a = 255
        elif isinstance(a, float): # clamp to range 0.0--1.0 and convert to integer 0--255
            if a < 0.0:
                a = 0
            elif a > 1.0:
                a = 255
            else:
                a = int(round(a*255))
        else:
            raise ValueError('Arguments must be integers or floats.')
        return a
    r = _chkarg(r)
    g = _chkarg(g)
    b = _chkarg(b)
    return '#{:02x}{:02x}{:02x}'.format(r,g,b)    

# prirad farby clustrom

def watch(r, g, b):
    if r > 250:
        r = 20
    if g > 250:
        g = 30
    if b > 250:
        b = 40 

    return r, g, b

def generate_color_for_clusters(list_cluster):
    
    num_colors = len(list_cluster)

    r, g, b = 0, 0, 0

    for i in range(0, num_colors):

        coordinates = list_cluster[i]
        x, y = coordinates[0], coordinates[1]
        cluster = board[x][y]
        color = htmlcolor(r, g, b)
        cluster.color = color
        if i % 2 == 0:
            r += 100
        elif i % 3 == 0:
            g += 150
        else:
            b += 100

        r, g, b = watch(r, g, b)

generate_color_for_clusters(list_coordinates)


# x-axis label
plt.xlabel('x - axis')
# frequency label
plt.ylabel('y - axis')
# plot title
plt.title('')

for coordinates in list_coordinates:
    x, y = coordinates[0], coordinates[1]
    cluster = board[x][y]

    list_x, list_y = [], []

    for point in cluster.list_points:
        list_x.append(point[0])
        list_y.append(point[1])

    plt.scatter(list_x, list_y, label= f"CN:{cluster.repr}", color=cluster.color, 
            marker= ".", s=50)
  

plt.legend()
  
# function to show the plot
plt.show()