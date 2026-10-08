# Draws thunderstore/icon.png (256x256): a fermenter barrel with an orange countdown clock.
# python3 thunderstore/make_icon.py   (needs Pillow)
import math, os
from PIL import Image, ImageDraw

S = 4  # draw at 4x, then shrink for smooth edges
W = 256 * S
img = Image.new("RGBA", (W, W), (0, 0, 0, 0))
d = ImageDraw.Draw(img)
d.rounded_rectangle((8 * S, 8 * S, 248 * S, 248 * S), radius=40 * S, fill=(24, 32, 48, 255), outline=(70, 90, 120, 255), width=4 * S)
# Barrel: bulging staves with iron hoops.
x0, x1, y0, y1 = 38, 150, 52, 212
d.rounded_rectangle((x0 * S, y0 * S, x1 * S, y1 * S), radius=34 * S, fill=(134, 88, 50, 255), outline=(70, 44, 24, 255), width=4 * S)
for x in range(x0 + 20, x1 - 10, 20):
    d.line([(x * S, (y0 + 6) * S), (x * S, (y1 - 6) * S)], fill=(104, 66, 36, 255), width=3 * S)
for y in (y0 + 26, (y0 + y1) // 2, y1 - 26):
    d.rectangle(((x0 - 2) * S, (y - 6) * S, (x1 + 2) * S, (y + 6) * S), fill=(92, 98, 108, 255), outline=(52, 56, 64, 255), width=2 * S)
# Clock, bottom right: orange ring, hands at about twenty to.
cx, cy, r = 178 * S, 170 * S, 52 * S
d.ellipse((cx - r, cy - r, cx + r, cy + r), fill=(24, 32, 48, 255))
d.ellipse((cx - r + 8 * S, cy - r + 8 * S, cx + r - 8 * S, cy + r - 8 * S), fill=(236, 230, 214, 255), outline=(240, 150, 40, 255), width=9 * S)
for k in range(12):
    a = math.radians(k * 30)
    rr = r - 20 * S
    d.ellipse((cx + rr * math.sin(a) - 3 * S, cy - rr * math.cos(a) - 3 * S, cx + rr * math.sin(a) + 3 * S, cy - rr * math.cos(a) + 3 * S), fill=(60, 60, 60, 255))
d.line([(cx, cy), (cx, cy - 30 * S)], fill=(40, 40, 40, 255), width=6 * S)
a = math.radians(230)
d.line([(cx, cy), (cx + 20 * S * math.sin(a), cy - 20 * S * math.cos(a))], fill=(40, 40, 40, 255), width=7 * S)
d.ellipse((cx - 6 * S, cy - 6 * S, cx + 6 * S, cy + 6 * S), fill=(240, 150, 40, 255))
img.resize((256, 256), Image.LANCZOS).save(os.path.join(os.path.dirname(os.path.abspath(__file__)), "icon.png"))
