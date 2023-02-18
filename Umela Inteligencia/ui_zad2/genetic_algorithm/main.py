import sys
import string
from random import randint

from utils.init_utils import generate_map
from utils.gen_utils import create_first_population, tournament_selection, calc_fitness, make_mutations, find_best_fitness, print_result


if len(sys.argv) != 4:
    exit("You have to specify: num_pop, num_city, size_of_map")

if len(sys.argv) == 2:
    SIZE_MAP = int(sys.argv[1])
    NUM_POP = randint(20, SIZE_MAP)
    NUM_CITY = randint(30, SIZE_MAP-2)
elif len(sys.argv) == 4:
    SIZE_MAP = int(sys.argv[1])
    NUM_POP = int(sys.argv[2])
    NUM_CITY = int(sys.argv[3])
elif len(sys.argv) == 3:
    SIZE_MAP = randint(50, 150)
    NUM_POP = int(sys.argv[1])
    NUM_CITY = int(sys.argv[2])
else:
    SIZE_MAP = randint(50, 150)
    NUM_POP = randint(20, 100)
    NUM_CITY = randint(50, 80)
    
NUM_GENERATIONS = 100
LIMIT = 15

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

convergence = False
best = population[find_best_fitness(population)]
count = 0

while(not convergence and LIMIT >= 0):
      population = tournament_selection(population, NUM_POP)
      make_mutations(population)
      calc_fitness(population, city_list)
      cur_best = population[find_best_fitness(population)]
      LIMIT -= 1
      
      if cur_best.get_fitness() > best.get_fitness():
            best = cur_best
      if cur_best.get_fitness() == best.get_fitness():
            count += 1
      if count > 15:
            convergence = True
      
print_result(best, board, city_list)


