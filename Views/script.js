document.addEventListener("DOMContentLoaded", () => {
    const loginForm = document.getElementById("login-form");
    const registerForm = document.getElementById("register-form");
    const toggleLink = document.getElementById("toggle-link");
    const authTitle = document.getElementById("auth-title");

    toggleLink.addEventListener("click", (event) => {
        event.preventDefault();
        if (loginForm.style.display === "none") {
            loginForm.style.display = "block";
            registerForm.style.display = "none";
            authTitle.textContent = "Login";
            toggleLink.textContent = "Register here";
        } else {
            loginForm.style.display = "none";
            registerForm.style.display = "block";
            authTitle.textContent = "Register";
            toggleLink.textContent = "Login here";
        }
    });

    loginForm.addEventListener("submit", async (event) => {
        event.preventDefault();
        const username = document.getElementById("login-username").value;
        const password = document.getElementById("login-password").value;
    
        const response = await fetch('/api/auth/login', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ username, password })
        });
    
        const result = await response.json();
        console.log(result);
    
        if (response.ok) {
            transformToWelcomeScreen(username, result.token);
        } else {
            alert(result.message || "Login failed");
        }
    });
    

    registerForm.addEventListener("submit", async (event) => {
        event.preventDefault();
        const username = document.getElementById("register-username").value;
        const email = document.getElementById("register-email").value;
        const password = document.getElementById("register-password").value;

        const response = await fetch('/api/auth/register', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ username, email, password })
        });

        const result = await response.json();

        if (response.ok) {
            transformToWelcomeScreen(username, "Token will be shown upon login.");
        } else {
            alert(result.message || "Registration failed");
        }
    });

    const transformToWelcomeScreen = (username, token) => {
        document.body.innerHTML = `
            <div id="welcome-container">
                <h1>Welcome, ${username}!</h1>
                <p>Your unique JWT token:</p>
                <div id="jwt-token">${token}</div>
                <div id="button-container">
                    <button id="logout-button">Logout</button>
                    <button id="delete-button">Delete Account</button>
                </div>
            </div>
        `;
    
        document.getElementById("logout-button").addEventListener("click", () => {
            logOut();
        });
    
        document.getElementById("delete-button").addEventListener("click", () => {
            deleteAccount(username);
        });
    };
    
    

    const logOut = () => {
        location.reload();
    };

    const deleteAccount = async (username) => {
        if (confirm("Are you sure you want to delete your account? This action cannot be undone.")) {
            const response = await fetch(`/api/auth/delete/${username}`, {
                method: 'DELETE',
                headers: { 'Content-Type': 'application/json' }
            });

            const result = await response.json();

            if (response.ok) {
                alert(result.message || "Account deleted successfully.");
                logOut();
            } else {
                alert(result.message || "Failed to delete account.");
            }
        }
    };
});
