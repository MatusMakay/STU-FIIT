class City:
    """
    Constructor sets random coordinates in range [0, size_map] and pick random char from string and pop it out and append it in city_names.
    Usage: get_cordinates(), get_name()
    """
    def __init__(self, x, y, name):
        self.name = name
        self.set_cordinates(x, y)
        
    def __str__(self):
        return f"{self.name}"

    def set_cordinates(self, x, y):
        self.cordinates = (x, y)

    def get_cordinates(self):
        return self.cordinates[0], self.cordinates[1]

    def get_cordinates_euclid(self):
        return (self.cordinates[0], self.cordinates[1])
    
    def get_name(self):
        return self.name
