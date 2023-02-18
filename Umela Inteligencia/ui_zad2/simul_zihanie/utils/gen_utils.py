from inspect import Attribute
import random
import copy
from subprocess import list2cmdline

from cls.Individual import Individual


"""
TODO vybrat rodicov z populacie, random 
"""



def calc_fitness(list, city_list):
    for individual in list: 
        individual.calc_fitness(city_list)


def make_mutations(list):
    for individual in list:
        individual.mutate()
        individual.mutate()
        individual.mutate()
        individual.mutate()
        individual.mutate()
        individual.mutate()

def find_best_fitness(list):
    max = 0
    max_idx = 0
    for idx, individual in enumerate(list):
        if individual.get_fitness() > max:
            max = individual.get_fitness()
            max_idx = idx
    
    return max_idx

def create_new_pop(parent, NUM_POP):
    population = []

    for i in range(0, NUM_POP):
        population.append(crossover(parent))

    return population



def pick_random(population):
    rand = random.randint(0, len(population)-1)
    return population[rand]

def crossover(parent):

    rand_idx = random.randint(1, len(parent.get_chromosone())-2)
    chromosone = parent.get_chromosone()

    new_chromosone = []
    chrom1 = chromosone[0: rand_idx]
    chrom2 = chromosone[rand_idx:]
    
    for i in range(0, len(chrom2)):
        new_chromosone.append(chrom2[i])

    for i in range(0, len(chrom1)):
        new_chromosone.append(chrom1[i])
    return Individual(new_chromosone)


# initial population
def generate_first_chromosone(city_names):
    
    tmp = copy.deepcopy(city_names)
    chromosone = []

    while(len(tmp) > 1):
        ran = random.randint(0, len(tmp)-1) 
        rand_char = tmp[ran]
        chromosone.append(rand_char)
        tmp.remove(rand_char)

    chromosone.append(tmp.pop())
    
    return chromosone

def create_first_population(size, city_names):
    """
    create first population and generate random chromozone from city_names
    """
    population = []
    for idx in range(0, size):
        individual = Individual(generate_first_chromosone(city_names))
        population.append(individual)
    
    return population



