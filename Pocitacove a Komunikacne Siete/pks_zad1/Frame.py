from os import sep


class Frame:
    
    def format_arr_str(self, arr):

        move = 2

        for idx, s in enumerate(arr):
            tmp_arr = []
            while len(s) > 2:
                tmp_arr.append(s[:move])
                s = s[move:]
            tmp_arr.append(s)
            arr[idx] = " ".join(tmp_arr).upper()

        #"\nbefore"
        arr = "\n".join(arr) + "\n"

        return arr

    
    def split_hex_frame_into_arr_str(self, hex_frame):
        hex_str = []
        tmp = hex_frame
        
        while len(tmp) > 32 :
            tmp_l = tmp[0 : 32]
            tmp = tmp[32:]

            hex_str.append(tmp_l)
        
        hex_str.append(tmp)
        return hex_str
    
    def set_hex_frame(self, hex_frame):
        return self.format_arr_str(self.split_hex_frame_into_arr_str(hex_frame))

