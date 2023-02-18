from exc.error import Error

class DamagedPacket(Error):
    """
    Occurs when checksum of data sended didnt equals to checksum in header
    """
    pass