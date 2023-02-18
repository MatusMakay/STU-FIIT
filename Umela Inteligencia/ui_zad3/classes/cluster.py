
#from scipy.spatial import distance
import math
class Cluster:

    """
    TODO ked pridam bod do clustru musim ho odstranit z mapy a do mapy zapisat len centroid/monoid
    """

    def __init__(self, x, y, repr, type):
        self.x = x
        self.y = y

        #   CENTROID, MEDOID
        self.type = type
        self.repr = repr
        self.color = None

        # tuples
        self.list_points = []
        self.list_points.append((x, y))


    def find_medoid(self):
        """
        Vyuzivam pre najdenie najblizsie bodu pri spajani klustrov
        """
        x, y = self.find_centroid()
        centroid = (x, y)

        min_len = math.dist(centroid, self.list_points[0])
        min_cord = (self.list_points[0])

        for cordinates in self.list_points:
            if min_cord != cordinates:
                euc_distance = math.dist(centroid, cordinates)
                
                if euc_distance < min_len:
                    min_len = euc_distance
                    min_cord = cordinates
            
        return min_cord[0], min_cord[1]

    def calc_average_distance_from_centroid(self):
        average_dis = 0
        num_points_in_cluster = len(self.list_points)
        
        c_x, c_y = self.find_centroid()
        centroid = (c_x, c_y)

        for point in self.list_points:
            average_dis += math.dist(centroid, point)
        
        average_dis/=num_points_in_cluster

        return average_dis


    def find_centroid(self, ):
        sum_x = 0
        sum_y = 0
        count_points = len(self.list_points)

        for cordinates in self.list_points:
            sum_x += cordinates[0]
            sum_y += cordinates[1]
        
        centroid_x = round(sum_x / count_points)
        centroid_y = round(sum_y / count_points)

        return centroid_x, centroid_y

    def calc_new_repr_cord(self,):
        """
        Calculate and returns new representative coordinates for MEDOID or CENTROID
        """
        if self.type == "CENTROID":
            self.x, self.y = self.find_centroid()    
        else:
            self.x, self.y = self.find_medoid()
        
        return self.x, self.y

    def add_cluster_points(self, points):
        """
        Method has to get cluster which is going to be removed from a map
        Append point in list of points and calculate new cord representation for cluster.
        Removes new point from map and set on map new representative coordinates
        """
        old_cordinates = self.get_cordinates()

        for point in points:
            self.list_points.append(point)

        new_x, new_y = self.calc_new_repr_cord()
        return old_cordinates[0], old_cordinates[1], new_x, new_y


    def get_cordinates(self):
        return (self.x, self.y)

    def __str__(self, ):
        return "%d" % self.repr