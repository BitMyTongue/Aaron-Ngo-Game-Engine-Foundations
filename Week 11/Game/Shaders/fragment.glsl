#version 330 core
out vec4 FragColor;

in vec3 FragPos;
in vec3 Normal;
in vec2 TexCoord;

uniform vec3 lightPos;        // Lamp position
uniform vec3 viewPos;         // Camera position
uniform vec3 lightColor;      // Lamp color
uniform sampler2D ourTexture; // Texture sampler

void main()
{
    // Lighting
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(lightPos - FragPos);
    vec3 viewDir  = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);

    // Ambient
    float ambientStrength = 0.15;
    vec3 ambient = ambientStrength * lightColor;

    // Diffuse
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * lightColor;

    // Specular
    float specularStrength = 0.5;
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32.0);
    vec3 specular = specularStrength * spec * lightColor;

    // Using my texture instead of flat cube color
    vec3 objectColor = texture(ourTexture, TexCoord).rgb;

    // Light fall-off (further from light source = darker)
    float distance = length(lightPos - FragPos);
    float attenuation = 1.0 / (1.0 + 0.22 * distance + 0.20 * (distance * distance));

    ambient  *= attenuation;
    diffuse  *= attenuation;
    specular *= attenuation;

    // Combine Results
    vec3 result = (ambient + diffuse + specular) * objectColor;
    FragColor = vec4(result, 1.0);
}
