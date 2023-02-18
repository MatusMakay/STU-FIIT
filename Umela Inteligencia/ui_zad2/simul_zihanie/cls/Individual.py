import random
from scipy.spatial import distance


class Individual:

    # 1.produkovat potomkov -> reproduce
    # 2.vykonavat krizenie, mutacie

    def __init__(self, chromosone):
        self.chromosone = chromosone

    def __str__(self):
        return f"chromosone: {self.chromosone}"
        

    def get_length_chromosone(self):
        return len(self.chromosone)

    def get_chromosone(self):
        return self.chromosone
        
    def swap(self, f_idx, s_idx):
        self.chromosone[f_idx], self.chromosone[s_idx] = self.chromosone[s_idx], self.chromosone[f_idx]

   
    def mutate(self):
        f_idx = random.randint(0, len(self.chromosone)-1)
        s_idx = random.randint(0, len(self.chromosone)-1)

        self.swap(f_idx, s_idx)

    def find_citys_in_list(self, name_l, name_r, city_list):
        tmp = []
        
        for city in city_list:
            if city.get_name() == name_l or city.get_name() == name_r:
                tmp.append(city)

        return tmp[0], tmp[1]

    #na pocitanie euklidovej vzdialenosti pouzivam kniznicu scapi
    def calc_euclid_range(self, citya, cityb):
        return distance.euclidean(citya.get_cordinates_euclid(), cityb.get_cordinates_euclid())


    def calc_fitness(self, city_list):
        #cordinates city
        lenght = self.get_length_chromosone()
        self.fitness = 0
        
        #
        for idx in range(0, lenght - 1):
            name_l = self.chromosone[idx]
            name_r = self.chromosone[idx+1]
            citya, cityb = self.find_citys_in_list(name_l, name_r, city_list)

            self.fitness += self.calc_euclid_range(citya, cityb)
        
        #uzavrie slucku prve s poslednym mestom        
        name_f = self.chromosone[0]
        name_l = self.chromosone[lenght - 1]

        city_f, city_l = self.find_citys_in_list(name_f, name_l, city_list)
        
        self.fitness += self.calc_euclid_range(city_f, city_l)


    def get_fitness(self):
        return self.fitness
    
    
    def find_city_in_list(self, name, city_list):
        for city in city_list:
            if city.get_name() == name:
                return city

        return False


    def print_path(self, city_list):
        print("Path: ")
        for gen in self.chromosone:
            city = self.find_city_in_list(gen, city_list)
            print(city.get_name(), ":", city.get_cordinates_euclid(), end=', ')


        


        
    