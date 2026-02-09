import time
import matplotlib.pyplot as plt
from matplotlib.ticker import ScalarFormatter




# --- Method Implementation ---


def multiply(F, M):
    x = (F[0][0] * M[0][0]+ 
         F[0][1] * M[1][0])
    
    y = (F[0][0] * M[0][1]+ 
         F[0][1] * M[1][1])
    
    z = (F[1][0] * M[0][0]+ 
         F[1][1] * M[1][0])
    
    w = (F[1][0] * M[0][1]+ 
         F[1][1] * M[1][1])
    
    F[0][0] = x
    F[0][1] = y
    F[1][0] = z
    F[1][1] = w

def power(F, n):
    M = [[1,1], 
         [1,0]]
    
    for i in range(2, n+1):
        multiply(F,M)

def fib(n):
    F = [[1,1], 
         [1,0]]
    
    if n == 0:
        return 0
    power(F, n-1)

    return F[0][0]



# --- Empirical Analysis Setup ---
n_values = [501, 631, 794, 1000, 1259, 1585, 1995, 2512, 
            3162, 3981, 5012, 6310, 7943, 10000, 12589, 15849]

avg_times = []

# --- Terminal Output (Horizontal with 3 Repetitions) ---
print(f"{'n':<7} | {'Repetition 1':<12} | {'Repetition 2':<12} | {'Repetition 3':<12} | {'Average':<12}")
print("-" * 75)

for n in n_values:
    runs = []
    for _ in range(3):
        start = time.perf_counter()
        fib(n)
        end = time.perf_counter()
        runs.append(end - start)
    
    avg = sum(runs) / 3
    avg_times.append(avg)
    
    # Print in the requested horizontal format
    print(f"{n:<7} | {runs[0]:.8f} | {runs[1]:.8f} | {runs[2]:.8f} | {avg:.8f}")

# --- Graph Generation ---
plt.figure(figsize=(10, 6))
plt.plot(n_values, avg_times, label='Matrix Power Method', marker='s', color='blue', linewidth=1.5)

plt.title('Empirical Analysis: Matrix Power (Average of 3 Runs)')
plt.xlabel('n-th Fibonacci Term')
plt.ylabel('Time (s)')

# Force the Y-axis to stay in 0.000... format
plt.gca().yaxis.set_major_formatter(ScalarFormatter(useOffset=False))
plt.ticklabel_format(style='plain', axis='y')

plt.grid(True, linestyle='--', alpha=0.6)
plt.legend()
plt.xticks([0, 2000, 4000, 6000, 8000, 10000, 12000, 14000, 16000])

plt.tight_layout()
plt.show()