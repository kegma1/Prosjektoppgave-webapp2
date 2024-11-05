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

    const mockApiRequest = (endpoint, data) => {
        return new Promise((resolve) => {
            setTimeout(() => {
                resolve({ success: true, message: `${endpoint} successful` });
            }, 1000);
        });
    };

    loginForm.addEventListener("submit", async (event) => {
        event.preventDefault();
        const username = document.getElementById("login-username").value;
        const password = document.getElementById("login-password").value;

        const response = await mockApiRequest("login", { username, password });
        if (response.success) {
            transformToWelcomeScreen(username);
        } else {
            alert("Login failed");
        }
    });

    registerForm.addEventListener("submit", async (event) => {
        event.preventDefault();
        const username = document.getElementById("register-username").value;
        const password = document.getElementById("register-password").value;

        const response = await mockApiRequest("register", { username, password });
        if (response.success) {
            transformToWelcomeScreen(username);
        } else {
            alert("Registration failed");
        }
    });

    const transformToWelcomeScreen = (username) => {
        document.body.innerHTML = `
            <div id="welcome-container">
                <h1>Welcome, ${username}!</h1>
                <p>You have successfully logged in.</p>
            </div>
        `;
    };
});
