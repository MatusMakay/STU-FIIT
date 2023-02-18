from inspect import Attribute
import random
import copy

from cls.Individual import Individual
from cls.City import City


"""
TODO vybrat rodicov z populacie, random 
"""

def calc_fitness(list, city_list):
    for individual in list: 
        individual.calc_fitness(city_list)


def make_mutations(list):
    for individual in list:
        individual.mutate()

def find_best_fitness(list):
    max = 0
    max_idx = 0
    for idx, individual in enumerate(list):
        if individual.get_fitness() > max:
            max = individual.get_fitness()
            max_idx = idx
    
    return max_idx

def pick_parent(num, tmp, list, chromosone):
    
    picked_idx = []

    while num > 0: 
        idx = random.randint(0, len(list)-1)
        
        if idx not in picked_idx: 
            num -= 1
            tmp.append(list[idx])

        picked_idx.append(idx)

    while True:
        try:
            parent = tmp[find_best_fitness(tmp)]
        except IndexError:
            return False

        if parent.get_chromosone() != chromosone:
            return parent
        else:
            tmp.remove(parent)


def tournament_selection(list, num_population):
    best = list[find_best_fitness(list)]
    tmp = []
    new_population = []

    while(len(new_population) != num_population-1):
        
        num_individual = int(random.randint(1, len(list))/2 + 1)
        parent_a = pick_parent(num_individual, tmp, list, "")
        
        try:
            parent_b = pick_parent(num_individual, tmp, list, parent_a.get_chromosone())
            child = crossover(parent_a, parent_b)
            if child is not False:
                new_population.append(crossover(parent_a, parent_b))

        except AttributeError:
            continue
        
    new_population.append(best)

    return new_population    

def init_crossover(par):
    return random.randint(0, par.get_length_chromosone()-1), par.get_length_chromosone()

def add_gen_in_child_chrom(l_idx, h_idx, child_chrom, par_chrom):
    for idx in range(l_idx, h_idx):
        
        par_gen = par_chrom[idx]
        check = child_chrom.count(par_gen)
        
        if check == 0:
            child_chrom.append(par_gen)


def crossover(par_a, par_b):

    if par_a is False or par_b is False:
        return False
    
    split_idx, length = init_crossover(par_a)
    
    chrom_child = []
    chrom_para = par_a.get_chromosone()
    chrom_parb = par_b.get_chromosone()

    for idx in range(0, split_idx):
        chrom_child.append(chrom_para[idx])

    add_gen_in_child_chrom(split_idx, length, chrom_child, chrom_parb)        
    if chrom_child != length:
        add_gen_in_child_chrom(0, split_idx, chrom_child, chrom_parb)
    
    return Individual(chrom_child)

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


def print_result(best, map, city_list):
    
    print("Map: ")
    for row, idx_x in enumerate(map):
        print(row, end=": ")
        for idx_y in idx_x:
            
            if isinstance(idx_y, City):
                print(idx_y.get_name(), end=", ")
                continue
            print(0, end=", ")
        print("")
    print("")

    print("")

    print("Best fitness:", round(1/best.get_fitness(), 2))
    print(f"Best chromozone: {' '.join([str(elem) for elem in best.get_chromosone()])}")

    best.print_path(city_list)


