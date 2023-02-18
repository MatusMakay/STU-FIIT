from cmath import exp
from utils.init_utils import generate_map, print_result
from utils.gen_utils import create_first_population,  calc_fitness, find_best_fitness, make_mutations, pick_random, create_new_pop

import string
import sys
from random import randint

from numpy.random import rand



if len(sys.argv) == 2:
    SIZE_MAP = int(sys.argv[1])
    NUM_POP = randint(20, SIZE_MAP)
    NUM_CITY = randint(30, SIZE_MAP-2)
    INIT_TMP=1000
elif len(sys.argv) == 4:
    SIZE_MAP = int(sys.argv[1])
    NUM_POP = int(sys.argv[2])
    NUM_CITY = int(sys.argv[3])
    INIT_TMP=1000
elif len(sys.argv) == 3:
    SIZE_MAP = randint(50, 150)
    NUM_POP = int(sys.argv[1])
    NUM_CITY = int(sys.argv[2])
    INIT_TMP=1000
elif len(sys.argv) == 5:
    SIZE_MAP = int(sys.argv[1])
    NUM_POP = int(sys.argv[2])
    NUM_CITY = int(sys.argv[3])
    INIT_TMP = int(sys.argv[4])
else:
    SIZE_MAP = randint(50, 150)
    NUM_POP = randint(20, 100)
    NUM_CITY = randint(20, 52)
    INIT_TMP=1000
    


ITERATIONS = 1000

print(NUM_CITY, SIZE_MAP, NUM_POP, INIT_TMP, sep=", ")
#contains every single char from A-Za-z
choices = string.ascii_letters
#stores used chars from choices
city_names = []
city_list = []

board = []

#created board, and city names
board, city_list = generate_map(SIZE_MAP, NUM_CITY, city_list, city_names, choices)

population = create_first_population(NUM_POP, city_names)

calc_fitness(population, city_list)

best_s = population[find_best_fitness(population)]
cur_b = best_s

count = 1

for idx in range(0, ITERATIONS):
    while True:
        
        if len(population) < NUM_POP/2:
            print_result(best_s, board, city_list)
            exit(5)

        candidate = pick_random(population)
        
        if candidate.get_fitness() < best_s.get_fitness():
            best_s = candidate
            cur_b = candidate
            

        cur_temp = INIT_TMP / (idx + 1)

        dif = candidate.get_fitness() - cur_b.get_fitness()

        criteria = exp(-dif/cur_temp)
        number = rand()

        if(dif < 0 or number < criteria.real):
            cur_b = candidate
            break

        population.remove(candidate)

    population = create_new_pop(cur_b, NUM_POP)

    make_mutations(population)
    calc_fitness(population, city_list)


print_result(cur_b, board, city_list)



print(generate_map(15, 5, city_list, city_names, string.ascii_letters))